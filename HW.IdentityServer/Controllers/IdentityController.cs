using FluentValidation.Results;
using HW.CustomerModels;
using HW.Http;
using HW.IdentityServer.Models;
using HW.IdentityServer.Models.Facebook;
using HW.IdentityServerModels;
using HW.IdentityViewModels;
using HW.PackagesAndPaymentsViewModels;
using HW.ReportsViewModels;
using HW.UserViewModels;
using HW.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HW.Utility.ClientsConstants;

namespace HW.IdentityServer.Controllers
{
    //[Authorize]
    //[Route("[controller]/[action]")]
    public class IdentityController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AspNetUserManager<ApplicationUser> _netUserManager;
        private readonly ILogger _logger;
        private readonly IHttpClientService httpClient;
        private readonly FbCredentials facebookCredentials;
        private readonly IExceptionService Exc;
        private readonly HWIdentityContext _identityContext;
        private readonly IUnitOfWork uow;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IdentityController> logger,
            IHttpClientService httpClient,
            FbCredentials facebookCredentials,
            IExceptionService Exc,
            HWIdentityContext identityContext,
            AspNetUserManager<ApplicationUser> netUserManager,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _netUserManager = netUserManager;
            this.httpClient = httpClient;
            this.facebookCredentials = facebookCredentials;
            this.Exc = Exc;
            _identityContext = identityContext;
            uow = unitOfWork;
        }

        [HttpGet]
        public string Start()
        {
            return "Identity Start successfully";
        }

        [HttpPost]
        [AllowAnonymous]
        public Response CheckEmailandPhoneNumberAvailability([FromBody] UserRegisterVM model)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                string id = "";
                List<ErrorModel> errors = new List<ErrorModel>();
                ValidationResult validationResult = new UserVmValidator(
                    !string.IsNullOrEmpty(model.AppleUserId) ||
                    !string.IsNullOrEmpty(model.FacebookUserId) ||
                    !string.IsNullOrEmpty(model.GoogleUserId), model.FromPersonalDetails).Validate(model);

                if (validationResult.IsValid)
                {
                    //string authId = (!string.IsNullOrEmpty(model.FacebookUserId) ? model.FacebookUserId : model.GoogleUserId) + "";
                    int roleId = (int)(Role)Enum.Parse(typeof(Role), model.Role);
                    int subroleId = 0;
                    if (!string.IsNullOrEmpty(model.subrole))
                    {
                        subroleId = (int)(Role)Enum.Parse(typeof(Role), model.subrole);
                    }
                    string email = !string.IsNullOrEmpty(model.Email) ? model.Email : "email";
                    UserIdVM existingUser = CheckUserExistance(email, model.PhoneNumber, model.FacebookUserId, model.GoogleUserId, model.AppleUserId, roleId, subroleId);
                    //success = string.IsNullOrEmpty(userId);

                    if (existingUser == null)
                    {
                        success = true;
                    }
                    else
                    {
                        if (!(string.IsNullOrWhiteSpace(model.Email)) && existingUser.Email == model.Email)
                        {
                            errors.Add(new ErrorModel { Key = "DuplicateEmail", Description = "That email is already registered. Try to login with it or try with a different email address." });
                            response.Message = "This email is already registered. Try to login with it or try with a different email address.";

                        }

                        if (existingUser.PhoneNumber == model.PhoneNumber)
                        {
                            errors.Add(new ErrorModel { Key = "DuplicatePhoneNumber", Description = "That mobile number is already registered. Try to login with it or try with a different phone number." });
                            response.Message = "This mobile number is already registered. Try to login with it or try with a different phone number.";

                        }

                        if ((string.IsNullOrWhiteSpace(model.Email)) && (string.IsNullOrWhiteSpace(existingUser.Email)))
                        {
                            response.Message = "This mobile number is already registered. Try to login with it or try with a different phone number.";
                        }

                        if ((!(string.IsNullOrWhiteSpace(model.Email)) && !(string.IsNullOrWhiteSpace(existingUser.Email))) && existingUser.PhoneNumber == model.PhoneNumber)
                        {
                            response.Message = " Email & Mobile number is already registered.";

                        }
                    }
                }
                else
                {
                    errors = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Provided email and phone number are available for registration.";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.ResultData = errors;
                }

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Response> RegisterUser([FromBody] UserRegisterVM model)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();

                bool isSocialLogin = !string.IsNullOrEmpty(model.AppleUserId) || !string.IsNullOrEmpty(model.FacebookUserId) || !string.IsNullOrEmpty(model.GoogleUserId);

                ValidationResult validationResult = new UserVmValidator(isSocialLogin).Validate(model);

                var aspUserName = "";
                if (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.PhoneNumber))
                {
                    aspUserName = model.FacebookUserId;
                    isSocialLogin = false;
                }
                else if (!string.IsNullOrEmpty(model.Email))
                {
                    aspUserName = model.Email;
                }
                else
                {
                    aspUserName = model.PhoneNumber;
                }

                if (validationResult.IsValid)
                {
                    var hasPassword = Password_has_with_salt(model.Password);
                    string guidId = Guid.NewGuid().ToString();
                    AspNetUsers aspNetUsers = new AspNetUsers()
                    {

                        UserName = aspUserName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        EmailConfirmed = isSocialLogin,
                        CreatedOn = DateTime.UtcNow,
                        PasswordHash = hasPassword + "",
                        FirebaseClientId = model.FirebaseClientId,
                        FacebookUserId = model.FacebookUserId,
                        GoogleUserId = model.GoogleUserId,
                        AppleUserId = model.AppleUserId,
                        PhoneNumberConfirmed = false,
                        Id = guidId,
                        SecurityStamp = guidId,
                    };

                    await uow.Repository<AspNetUsers>().AddAsync(aspNetUsers);
                    await uow.SaveAsync();

                    //response.Status = ResponseStatus.OK;

                    //var identityResult = await _userManager.CreateAsync(user, model.Password);
                    string roleId = string.Empty;

                    if (!string.IsNullOrWhiteSpace(aspNetUsers.Id))
                    {
                        model.Id = aspNetUsers.Id;

                        switch (model.Role)
                        {
                            case "Tradesman":
                                roleId = "1";
                                break;
                            case "Organization":
                                roleId = "2";
                                break;
                            case "Customer":
                                roleId = "3";
                                break;
                            case "Supplier":
                                roleId = "4";
                                break;
                            case "Admin":
                                roleId = "5";
                                break;
                            default:
                                break;
                        }
                        //AspNetUserRoles aspNetUserRoles = new AspNetUserRoles
                        //{
                        //    RoleId = roleId,
                        //    UserId = model.Id,
                        //};
                        //uow.Repository<AspNetUserRoles>().Add(aspNetUserRoles);
                        //uow.Save();
                        AspNetUserRoles aspNetUserRoles = new AspNetUserRoles
                        {
                            RoleId = roleId,
                            UserId = model.Id,
                            SellerType = model.SellerType,
                            SecurityRole = 0
                        };

                        SqlParameter[] sqlParameter = {
                        new SqlParameter("@roleId",aspNetUserRoles.RoleId),
                        new SqlParameter("@userId",aspNetUserRoles.UserId),
                        new SqlParameter("@sellerType",aspNetUserRoles.SellerType),
                        new SqlParameter("@securityRole",aspNetUserRoles.SecurityRole)
                        };

                        uow.ExecuteReaderSingleDS<AspNetUserRoles>("Sp_AddAspNetUserRole", sqlParameter);
                        if (roleId == "5")
                        {
                            success = AddUpdateAdminRoleDetails(model.Id, roleId, model.SecurityRole, model.UserName);
                        }
                        //success = true;

                        model.Id = aspNetUsers.Id;
                        response.ResultData = model;

                        success = UpdateHasPin(model.Id, model.TermsAndConditions);
                        //var aa = await _userManager.FindByIdAsync(model.Id);

                        //aa.HasPIN = true;

                        //await _userManager.UpdateAsync(aa);

                        //Sp_InsertAdminTypeUserRoles
                    }
                    else
                    {
                        //errors = identityResult.Errors.Select(e => new ErrorModel { Key = e.Code, Description = e.Description }).ToList();
                    }
                }
                else
                {
                    //errors = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Registration successful.";

                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Registration failed.";
                    response.ResultData = errors;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        [HttpGet]
        public async Task<Response> UpdateUsersPhoneNumberByUserId(string userId, string phoneNumber, string email)
        {
            Response response = new Response();

            try
            {
                AspNetUsers user = uow.Repository<AspNetUsers>().GetAll().Where(x => x.Id == userId).FirstOrDefault();

                if (user != null && phoneNumber != null)
                {
                    user.PhoneNumber = phoneNumber;
                    user.PhoneNumberConfirmed = true;
                    uow.Repository<AspNetUsers>().Update(user);
                    await uow.SaveAsync();

                    response.Message = $"{phoneNumber} is verified successfully";
                    response.ResultData = true;
                    response.Status = ResponseStatus.OK;
                }
                else if (user != null && email != null)
                {
                    user.EmailConfirmed = true;

                    uow.Repository<AspNetUsers>().Update(user);
                    await uow.SaveAsync();

                    response.Message = $"{email} is verified successfully";
                    response.ResultData = true;
                    response.Status = ResponseStatus.OK;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                response.Message = ex.Message;
                response.ResultData = false;
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        [HttpGet]
        public bool GetPhoneNumberVerificationStatus(string userId)
        {
            bool scucessStatus = false;
            SqlParameter[] sqlParameter =
            {
                  new SqlParameter("@userId",userId),
              };
            VerificationStatusVM status = uow.ExecuteReaderSingleDS<VerificationStatusVM>("GetPhoneNumberVerificationStatus", sqlParameter)?.FirstOrDefault();
            if (status?.PhoneNumberConfirmed == true)
            {
                scucessStatus = true;
            }
            else
            {
                scucessStatus = false;
            }


            return scucessStatus;


        }

        public static string Password_has_with_salt(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
            //var password_byte = ASCIIEncoding.ASCII.GetBytes(password);
            //byte[] data_input = new byte[password_byte.Length];
            //for (int i = 0; i < password_byte.Length; i++)
            //{
            //    data_input[i] = password_byte[i];
            //}
            //SHA512 shaM = new SHA512Managed();
            //var hashed_byte_array = shaM.ComputeHash(data_input);
            //string hashed_result = Convert.ToBase64String(hashed_byte_array);
            //return new Tuple<byte[], string>(new byte[20], hashed_result);
        }

        public async Task<Response> GetUserByUserId(string userId)
        {
            Response response = new Response();
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "UserId is required.";
                }
                else
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);


                    if (user != null)
                    {
                        IList<string> userRoles = await _userManager.GetRolesAsync(user);

                        UserRegisterVM userVM = new UserRegisterVM()
                        {
                            Id = user.Id,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            FirebaseClientId = user.FirebaseClientId,
                            IsEmailConfirmed = user.EmailConfirmed,
                            IsNumberConfirmed = user.PhoneNumberConfirmed,
                            FacebookUserId = user.FacebookUserId,
                            GoogleUserId = user.GoogleUserId,
                            Role = userRoles != null && userRoles.Count > 0 ? userRoles.FirstOrDefault() : null
                        };

                        response.Status = ResponseStatus.OK;
                        response.ResultData = userVM;
                    }
                    else
                    {
                        response.Message = "The specified user does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {

                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public Response GetUserIdByEmailOrPhoneNumber(string emailOrPhoneNumber, Role userRoles)
        {
            Response response = new Response();
            try
            {
                var userVM = new UserRegisterVM();
                bool mobileNumberProvided = false;
                bool emailProvided = false;

                if (string.IsNullOrWhiteSpace(emailOrPhoneNumber))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Phone number is required.";
                }
                else
                {
                    List<UserIdVM> userIdVMs;

                    Match emailMatch = CustomRegularExpressions.Email().Match(emailOrPhoneNumber);
                    Match mobileNumberMatch = CustomRegularExpressions.MobileNumber().Match(emailOrPhoneNumber);

                    if (emailMatch.Success)
                    {
                        SqlParameter[] sqlParameter =
                        {
                            new SqlParameter("@email",emailOrPhoneNumber),
                            new SqlParameter("@phoneNumber","000"),
                            new SqlParameter("@userRole",(int)userRoles)
                        };

                        userIdVMs = uow.ExecuteReaderSingleDS<UserIdVM>("GetUserIdByEmailOrPhoneNumber", sqlParameter);
                        emailProvided = true;
                    }
                    else if (mobileNumberMatch.Success)
                    {
                        SqlParameter[] sqlParameter =
                        {
                            new SqlParameter("@email","nullEmail"),
                            new SqlParameter("@phoneNumber",emailOrPhoneNumber),
                            new SqlParameter("@userRole",(int)userRoles)
                        };

                        userIdVMs = uow.ExecuteReaderSingleDS<UserIdVM>("GetUserIdByEmailOrPhoneNumber", sqlParameter);
                        mobileNumberProvided = true;
                    }
                    else userIdVMs = new List<UserIdVM>();

                    if (userIdVMs?.Count > 0)
                    {
                        userVM = new UserRegisterVM()
                        {
                            Id = userIdVMs?.FirstOrDefault()?.Id
                        };

                        if (mobileNumberProvided)
                        {
                            userVM.PhoneNumber = emailOrPhoneNumber;
                        }
                        else if (emailProvided)
                        {
                            userVM.Email = emailOrPhoneNumber;
                        }

                        userVM.Role = GetRoleByUserId(userVM.Id);
                        response.Status = ResponseStatus.OK;
                        response.ResultData = userVM;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Your Phone Number have not Registerd Please try to use valid Phone Number";
                    }
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> GetPasswordResetToken(string userId)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();


                if (string.IsNullOrWhiteSpace(userId))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "UserId is required.";
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                        response.ResultData = passwordResetToken;
                        success = true;
                    }
                    else
                    {
                        response.Message = "The specified user does not exist.";
                    }
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.ResultData = errors;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<Response> DeleteUser(ApplicationUser user)
        {
            Response response = new Response();
            try
            {
                var identityResult = await _userManager.DeleteAsync(user);
                if (identityResult.Succeeded)
                {
                    response.Status = ResponseStatus.OK;
                    response.ResultData = null;
                    response.Message = "User deleted successfully.";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.ResultData = null;
                    response.Message = "User deletion failed.";
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Response> ResetPassword([FromBody] ResetPasswordVM model)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();
                ValidationResult validationResult = new ResetPasswordVmValidator().Validate(model);

                if (validationResult.IsValid)
                {
                    AspNetUsers user = uow.Repository<AspNetUsers>().Get(x => x.Id == model.UserId).FirstOrDefault();

                    if (user != null)
                    {
                        var hasPassword = Password_has_with_salt(model.Password);

                        user.ModifiedOn = DateTime.UtcNow;
                        user.ModifiedBy = model.UserId;
                        user.PasswordHash = hasPassword + "";

                        uow.Repository<AspNetUsers>().Update(user);
                        await uow.SaveAsync();

                        success = UpdateHasPin(model.UserId, true);
                    }
                    else
                    {
                        errors.Add(new ErrorModel { Key = "InvalidUserId", Description = "The specified user does not exist." });
                    }
                }
                else
                {
                    errors = validationResult.Errors.Select(e => new ErrorModel { Key = e.PropertyName, Description = e.ErrorMessage }).ToList();
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                    response.Message = "Password has been successfully changed.";
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Password reset failed.";
                    response.ResultData = errors;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> UpdateUserFirebaseToken(string userId, string firebaseToken)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();


                if (string.IsNullOrWhiteSpace(userId))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "UserId is required.";
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        user.FirebaseClientId = firebaseToken;
                        user.ModifiedBy = userId;
                        user.ModifiedOn = DateTime.UtcNow;

                        var identityResult = await _userManager.UpdateAsync(user);
                        if (identityResult.Succeeded)
                        {
                            success = true;
                            response.ResultData = true;
                            response.Message = "Firebase Token is saved";
                        }
                        else
                        {
                            errors = identityResult.Errors.Select(e => new ErrorModel { Key = e.Code, Description = e.Description }).ToList();
                            response.Message = "Firebase Token is not updated";
                        }
                    }
                    else
                    {
                        response.Message = "The specified user does not exist.";
                    }
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.ResultData = errors;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> IsUserVerified(string userId)
        {
            Response response = new Response();
            try
            {
                bool success = false;
                List<ErrorModel> errors = new List<ErrorModel>();


                if (string.IsNullOrWhiteSpace(userId))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "UserId is required.";
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        success = true;

                        if (user.EmailConfirmed && user.PhoneNumberConfirmed)
                        {
                            response.ResultData = true;
                            response.Message = "Verified.";
                        }
                        else
                        {
                            response.ResultData = false;
                            response.Message = "Not verified";
                        }
                    }
                    else
                    {
                        response.Message = "The specified user does not exist.";
                    }
                }

                if (success)
                {
                    response.Status = ResponseStatus.OK;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.ResultData = errors;
                }

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return response;
        }

        public Response GetUserEmailByPhoneNumber(string phoneNumber)
        {
            Response response = new Response();
            try
            {
                var emailAddress = string.Empty;

                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Phone number is required.";
                }
                else
                {
                    ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

                    if (user != null)
                    {
                        response.Status = ResponseStatus.OK;
                        response.ResultData = user.Email;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "No record could be found for the specified phone number.";
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }

            return response;
        }

        public async Task<Response> UpdateUser([FromBody] UserRegisterVM model)
        {
            Response response = new Response();
            try
            {
                List<ErrorModel> errors = new List<ErrorModel>();

                if (model == null)
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "User is required.";
                }
                else
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                    //if (user != null && (user.Email == model.Email || user.PhoneNumber.Equals(model.PhoneNumber)))
                    if (user != null)
                    {
                        //AspNetUsers existingUser = GetUserByEmailOrPhoneNumber(model.Email, model.PhoneNumber, model.Role);
                        // long userId =Int64.Parse(model.Id);
                        AspNetUsers existingUser = GetUserByID(model.Id);

                        //if ((string.IsNullOrEmpty(existingUser?.Email) || existingUser?.Email == model.Email)
                        //   && (string.IsNullOrEmpty(existingUser?.PhoneNumber) || existingUser?.PhoneNumber == model.PhoneNumber))
                        if (existingUser != null)
                        {
                            existingUser.Email = model.Email;
                            existingUser.UserName = !string.IsNullOrEmpty(model.Email) ? model.Email : model.PhoneNumber;
                            existingUser.PhoneNumber = model.PhoneNumber;
                            existingUser.ModifiedBy = model.Id;
                            existingUser.ModifiedOn = DateTime.UtcNow;

                            //var identityResult = await _userManager.UpdateAsync(user);
                            uow.Repository<AspNetUsers>().Update(existingUser);
                            await uow.SaveAsync();
                            //if (identityResult.Succeeded)
                            //{
                            response.Status = ResponseStatus.OK;
                            response.ResultData = true;
                            response.Message = "Firebase Token is saved";
                            //}
                            //else
                            //{
                            //    response.Status = ResponseStatus.Error;
                            //    errors = identityResult.Errors.Select(e => new ErrorModel { Key = e.Code, Description = e.Description }).ToList();
                            //    response.Message = "Firebase Token is not updated";
                            //}
                        }
                        else
                        {
                            errors.Add(new ErrorModel { Key = "DuplicateEmail", Description = "That email is already registered. Try to login with it or try with a different email address." });
                            response.Status = ResponseStatus.Error;
                            response.Message = "Provided email and phone number are already registered.";
                            response.ResultData = errors;
                        }
                    }
                    else
                    {
                        response.Message = "Information Updated";
                        response.Status = ResponseStatus.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        public List<IdentityIdsValueVM> GetUserListByUserIds([FromBody] List<string> userIds)
        {
            List<IdentityIdsValueVM> userVM = new List<IdentityIdsValueVM>();

            try
            {
                if (userIds.Count > 0)
                {
                    var users = _netUserManager.Users.Where(x => userIds.Contains(x.Id)).ToList();

                    userVM = users.Select(x => new IdentityIdsValueVM
                    {
                        UserId = x.Id,
                        FirebaseId = x.FirebaseClientId
                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return userVM;
        }

        [HttpPost]
        public List<IdentityUserTypeVM> GetUsersTypeByUserIds([FromBody] List<string> userIds)
        {
            List<IdentityUserTypeVM> userVM = new List<IdentityUserTypeVM>();

            try
            {
                if (userIds.Count > 0)
                {
                    List<ApplicationUser> users = _netUserManager.Users.Where(x => userIds.Contains(x.Id)).ToList();

                    userVM = users.Select(x => new IdentityUserTypeVM
                    {
                        UserId = x.Id,
                        IsTestUser = x?.IsTestUser ?? false

                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return userVM;
        }


        public Response GetUserByGoogleId(string googleId)
        {
            Response response;
            try
            {
                if (string.IsNullOrEmpty(googleId))
                {
                    response = new Response
                    {
                        Message = "Google login failed",
                        ResultData = null,
                        Status = ResponseStatus.Error
                    };
                }
                else
                {
                    ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.GoogleUserId == googleId);

                    if (user != null)
                    {
                        response = new Response
                        {
                            Message = "Google login successfully",
                            ResultData = user,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response
                        {
                            Status = ResponseStatus.Restrected,
                            Message = "No record could be found for the specified google id.",
                            ResultData = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response
                {
                    Message = ex.Message,
                    ResultData = null,
                    Status = ResponseStatus.Restrected
                };
            }

            return response;
        }

        public Response GetUserNameByUserId(string userId)
        {
            try
            {
                var userName = _netUserManager.Users.FirstOrDefault(x => x.Id == userId).UserName;
                return new Response()
                {
                    Status = ResponseStatus.OK,
                    ResultData = userName
                };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }


        [HttpGet]
        public Response GetFirebaseIdByUserId(string userId)
        {
            try
            {
                string firebaseId = _userManager.Users.FirstOrDefault(x => x.Id == userId).FirebaseClientId;

                return new Response()
                {
                    Status = ResponseStatus.OK,
                    ResultData = firebaseId
                };
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        [HttpPost]
        public string Login([FromBody] LoginVM loginVM)
        {
            try
            {

                int roleId = (int)(Role)Enum.Parse(typeof(Role), loginVM.Role);

                UserIdVM user = CheckUserExistance(loginVM.EmailOrPhoneNumber, loginVM.EmailOrPhoneNumber, loginVM.FacebookClientId, loginVM.GoogleClientId, loginVM.AppleUserId, roleId,0);

                if (user != null)
                {
                    UserIdVM userIdVM = new UserIdVM
                    {
                        Id = user.Id,
                        FirebaseClientId = user.FirebaseClientId
                    };
                    
                    return JsonConvert.SerializeObject(userIdVM);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return string.Empty;
            }
        }

        [HttpGet]
        public UserIdVM CheckUserExistance(string email, string phoneNumber, string facebookId, string googleId, string appleUserId, int roleId, int subroleId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@email", email),
                    new SqlParameter("@phoneNumber",phoneNumber),
                    new SqlParameter("@facebookId",facebookId),
                    new SqlParameter("@appleUserId",appleUserId),
                    new SqlParameter("@googleId",googleId),
                    new SqlParameter("@roleId",roleId),
                    new SqlParameter("@subroleId",subroleId),       
                };

                UserIdVM result = uow.ExecuteReaderSingleDS<UserIdVM>("Sp_GetAspNetUser", sqlParameters)?.FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return null;
            }
        }

        //[HttpPost]
        //public async Task<Response> AdminResetPassword([FromBody] AdminResetPasswordVm adminResetPasswordVm)
        //{
        //    Response response = new Response();
        //    try
        //    {
        //        bool success = false;
        //        List<ErrorModel> errors = new List<ErrorModel>(); 
        //       var UserId = await httpClient.PostAsync($"{_apiConfig.IdentityServerApiUrl}{ApiRoutes.IdentityServer.Login}", adminResetPasswordVm);

        //        string passwordResetToken = string.Empty;
        //        if (string.IsNullOrWhiteSpace(adminResetPasswordVm.UserId))
        //        {
        //            response.Status = ResponseStatus.Error;
        //            response.Message = "UserId is required.";
        //        }
        //        else
        //        {
        //            var user = await _userManager.FindByIdAsync(adminResetPasswordVm.UserId);

        //            if (user != null)
        //            {
        //                passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        //                success = true;
        //            }
        //            else
        //            {
        //                response.Message = "The specified user does not exist.";
        //            }
        //        }

        //        if (success && !string.IsNullOrWhiteSpace(passwordResetToken))
        //        {
        //            var user = await _userManager.FindByIdAsync(adminResetPasswordVm.UserId);

        //            if (user != null)
        //            {
        //                user.ModifiedOn = DateTime.UtcNow;
        //                user.ModifiedBy = adminResetPasswordVm.UserId;

        //                var result = await _userManager.ResetPasswordAsync(user, passwordResetToken, adminResetPasswordVm.Passwords);

        //                if (result.Succeeded)
        //                {
        //                    success = true;
        //                }
        //                else
        //                {
        //                    errors = result.Errors.Select(e => new ErrorModel { Key = e.Code, Description = e.Description }).ToList();
        //                }
        //            }
        //            else
        //            {
        //                errors.Add(new ErrorModel { Key = "InvalidUserId", Description = "The specified user does not exist." });
        //            }
        //            if (success)
        //            {
        //                response.Status = ResponseStatus.OK;
        //                response.Message = "Password has been successfully changed.";
        //            }
        //            else
        //            {
        //                response.Status = ResponseStatus.Error;
        //                response.Message = "Password reset failed.";
        //                response.ResultData = errors;
        //            }
        //        }
        //        else
        //        {
        //            response.Status = ResponseStatus.Error;
        //            response.ResultData = errors;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exc.AddErrorLog(ex);
        //    }
        //    return response;
        //}

        public async Task<bool> ApplicationServerPing(string userId)
        {
            Response response = new Response();

            bool success = false;

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.LastActive = DateTime.Now;

                var identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    success = true;
                }
                else
                {
                    success = true;
                }
            }
            else
            {
                success = false;
            }

            return success;
        }

        [HttpGet]
        public string AdminForgotPasswordAuthentication([FromQuery] string email, string role)
        {
            try
            {
                SqlParameter[] sqlParameters =
                    {
                    new SqlParameter("@email", email),
                    new SqlParameter("@role", role),
                    };
                var result = uow.ExecuteReaderSingleDS<AdminForgotPasswordVM>("ForgotAdminPasswordId", sqlParameters).FirstOrDefault();
                return result?.UserId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return "";
            }
        }

        [HttpGet]
        public List<GetAdminUserDetails> GetAdminUserDetails(int roleId)
        {
            List<GetAdminUserDetails> getAdminUsers = new List<GetAdminUserDetails>();
            try
            {
                SqlParameter[] sqlParameters =
                    {
                    new SqlParameter("@roleId", roleId),
                    };
                getAdminUsers = uow.ExecuteReaderSingleDS<GetAdminUserDetails>("SP_GET_ASPNETUSERBYROLEID", sqlParameters).ToList();


            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

            }
            return getAdminUsers;
        }

        [HttpPost]
        public bool UpdateAdminUserdetails([FromBody] GetAdminUserDetails detailsVMs)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@id",detailsVMs.Id),
                new SqlParameter("@userName",detailsVMs.UserName),
                new SqlParameter("@email",detailsVMs.Email),
                new SqlParameter("@no",detailsVMs.PhoneNumber),
                new SqlParameter("@SecurityRole",detailsVMs.SecurityRole)
            };
            uow.ExecuteReaderSingleDS<GetAdminUserDetails>("UpdateUerAdminDetails", sqlParameter);
            return true;
        }

        [HttpPost]
        public bool AddUpdateAdminRoleDetails(string UserId, string RoleId, string SecurityRole, string UserName)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@RoleId",RoleId),
                new SqlParameter("@SecurityRole",SecurityRole)

            };
            uow.ExecuteReaderSingleDS<GetAdminUserDetails>("Sp_InsertAdminTypeUserRoles", sqlParameter);

            return true;
        }

        [HttpPost]
        public bool UpdateHasPin(string UserId, bool termsAndConditions)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@termsAndConditions",termsAndConditions),
            };
            uow.ExecuteReaderSingleDS<GetAdminUserDetails>("SP_UpdateHasPin", sqlParameter);

            return true;
        }

        [HttpGet]
        public bool DeleteAdminUser(string userId)
        {
            SqlParameter[] sqlParameter =
            {
                new SqlParameter("@id",userId)
            };
            uow.ExecuteReaderSingleDS<GetAdminUserDetails>("DeleteUserAdmin", sqlParameter);

            return true;
        }

        [HttpPost]
        public async Task<Response> ChangeAdminUserPassword([FromBody] ChangePasswordVM changePasswordVm)
        {
            Response res = new Response();
            //var findById =  await _userManager.FindByIdAsync(changePasswordVm.adminId);
            AspNetUsers user = uow.Repository<AspNetUsers>().GetAll().Where(x => x.Id == changePasswordVm.adminId).FirstOrDefault();
            if (user != null)
            {
                try
                {
                    var hashedPassword = Password_has_with_salt(changePasswordVm.newPassword);
                    user.PasswordHash = hashedPassword;
                    uow.Repository<AspNetUsers>().Update(user);
                    await uow.SaveAsync();
                    res.Status = ResponseStatus.OK;
                    res.Message = "Password updated successfully";

                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                }


                //var result = await _userManager.ChangePasswordAsync(findById , changePasswordVm.currentPassword , changePasswordVm.newPassword);
                //if(result.Succeeded)
                //{
                //    res.Status = ResponseStatus.OK;
                //    res.Message = "Password updated successfully";
                //}
                //else
                //{
                //    //res.Message = "error in changing password";
                //    var err = result.Errors.ToList();
                //    foreach (var item in err)
                //    {
                //        if(item != null && item.Code == "PasswordMismatch")
                //        {
                //            res.Message = item.Code;
                //        }
                //        else
                //        {
                //            res.Message = "something went wrong";
                //        }
                //    }
                //}
            }
            else
            {
                res.Status = ResponseStatus.Error;
                res.Message = "invalid user id";
            }
            return res;
        }

        [HttpGet]
        public string GetPhoneNumberByUserId(string userId)
        {
            try
            {
                var aa = _netUserManager.Users.FirstOrDefault(x => x.Id == userId).PhoneNumber;
                return aa;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return null;
            }
        }

        #region Facebook API
        public async Task<bool> ValidateFacebookToken(string facebookToken)
        {
            var response = false;

            if (!string.IsNullOrWhiteSpace(facebookToken))
            {
                try
                {
                    var tokenInfo = await GetTokenInfoAsync(facebookToken);
                    response = tokenInfo != null;       //if facebookToken provided by user is valid, return true
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                }
            }

            return response;
        }

        public async Task<FbUser> GetFbUserAsync(string facebookToken)
        {
            FbUser fbUser = null;

            if (!string.IsNullOrWhiteSpace(facebookToken))
            {
                try
                {
                    var tokenInfo = await GetTokenInfoAsync(facebookToken);

                    //if facebookToken provided by user is valid
                    if (tokenInfo != null)
                    {
                        var json = await httpClient.GetAsync($"{FacebookConstants.ApiBaseUrl}/{tokenInfo.Data.UserId}?access_token={facebookToken}&fields={string.Join(",", FacebookConstants.Scopes)}");
                        fbUser = JsonConvert.DeserializeObject<FbUser>(json);
                    }
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                }
            }

            return fbUser;
        }

        public async Task<FbUser> FindUserFbRecord(string facebookToken)
        {//to find and check if a user is already registered with us through his Facebook Account

            FbUser fbUser = null;

            if (!string.IsNullOrWhiteSpace(facebookToken))
            {
                try
                {
                    var tokenInfo = await GetTokenInfoAsync(facebookToken);

                    //if facebookToken provided by user is valid
                    if (tokenInfo != null)
                    {
                        //var json = await httpClient.GetAsync($"{FacebookConstants.ApiBaseUrl}/{tokenInfo.Data.UserId}?access_token={facebookToken}&fields={string.Join(",", FacebookConstants.Scopes)}");
                        //fbUser = JsonConvert.DeserializeObject<FbUser>(json);

                        ApplicationUser user = _userManager.Users.FirstOrDefault(u => u.FacebookUserId == tokenInfo.Data.UserId);

                        if (user != null)
                        {
                            fbUser = await GetFbUserAsync(facebookToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                }
            }

            return fbUser;
        }

        private async Task<FbAccessToken> GetFbAccessTokenAsync()
        {
            FbAccessToken fbAccessToken = null;

            try
            {
                fbAccessToken = JsonConvert.DeserializeObject<FbAccessToken>(
                    await httpClient.GetAsync($"{FacebookConstants.ApiBaseUrl}{FacebookConstants.GetAccessToken}".Replace("{{clientId}}", FacebookConstants.AppId).Replace("{{clientSecret}}", facebookCredentials.AppSecret))
                );
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return fbAccessToken;
        }

        [HttpGet]
        public List<InactiveUserVM> getAllInActiveUser(int timeDuration)
        {
            List<InactiveUserVM> inactiveUserVMs = new List<InactiveUserVM>();
            var lastActiveDate = DateTime.Today.AddDays(-timeDuration);
            inactiveUserVMs = _userManager.Users.Where(x => x.LastActive >= lastActiveDate).Select(i => new InactiveUserVM { UserId = i.Id, LastActive = i.LastActive }).ToList();
            return inactiveUserVMs;
        }

        [HttpGet]
        public List<SecurityRoleItemVM> GetSecurityRoleItem()
        {
            var result = uow.ExecuteCommand<SecurityRoleItemVM>("GetSecurityRoleItem");
            List<SecurityRoleItemVM> securityRoleItemVMs = result.ToList();
            return securityRoleItemVMs;
        }

        [HttpGet]
        public List<SecurityRoleItemVM> Changeusertype()
        {
            var result = uow.ExecuteCommand<SecurityRoleItemVM>("GetSecurityRoleItem");
            List<SecurityRoleItemVM> securityRoleItemVMs = result.ToList();
            return securityRoleItemVMs;
        }

        [HttpGet]
        public List<SecurityRoleVM> GetSecurityRoles()
        {
            var result = uow.ExecuteCommand<SecurityRoleVM>("GetSecurityRoles");
            List<SecurityRoleVM> securityRoleVMs = result.ToList();
            return securityRoleVMs;
        }

        [HttpGet]
        public Response DeleteSecurityRoleItem(int securityRoleItemId)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@id",securityRoleItemId)
                };
                uow.ExecuteReaderSingleDS<Response>("SP_DeleteSecurityRoleItem", sqlParameters);
                response.Status = ResponseStatus.OK;
                response.Message = "Successfully Deleted";
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }
        [HttpPost]
        public Response AddUpdateSecurityRoleItem([FromBody] SecurityRoleItemVM securityRoleItem)
        {
            try
            {
                Response response = new Response();
                if (securityRoleItem.SubRoleItem == null)
                {
                    securityRoleItem.SubRoleItem = 0;
                }
                SqlParameter[] sqlParameter =
                {
                    new SqlParameter("@id",securityRoleItem.Id),
                    new SqlParameter("@securityRoleItem",securityRoleItem.SecurityRoleItem),
                    new SqlParameter("@subRoleItem",securityRoleItem.SubRoleItem),
                    new SqlParameter("@isModule",securityRoleItem.IsModule),
                    new SqlParameter("@routingPath",securityRoleItem.RoutingPath),
                };
                var res = uow.ExecuteReaderSingleDS<Response>("SP_AddUpdateSecurityRoleItem", sqlParameter).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                response.Message = res.Message;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
        }

        [HttpGet]
        public List<GetSecurityRoleDetailsVM> GetSecurityRoleDetails(int roleId = 0, string UserId = "")
        {
            SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@roleId",roleId),
                    new SqlParameter("@UserId",UserId)
                    };
            var result = uow.ExecuteReaderSingleDS<GetSecurityRoleDetailsVM>("GetSecurityRoleDetails", sqlParameters);
            List<GetSecurityRoleDetailsVM> securityRoleDetailsVMs = result.ToList();
            return securityRoleDetailsVMs;
        }

        [HttpPost]
        public bool AddSecurityRoleDetails([FromBody] List<GetSecurityRoleDetailsVM> detailsVMs)
        {
            int count = 1;


            foreach (var item in detailsVMs)
            {
                SqlParameter[] sqlParameters =
             {
                    new SqlParameter("@status",count),
                    new SqlParameter("@Id",item.SecurityRoleId),
                    new SqlParameter("@page",item.SecurityRoleItems),
                    new SqlParameter("@view",item.AllowView),
                    new SqlParameter("@edit",item.AllowEdit),
                    new SqlParameter("@delete",item.AllowDelete),
                    new SqlParameter("@print",item.AllowPrint),
                    new SqlParameter("@export",item.AllowExport),
                    new SqlParameter("@spectialrights",item.spectialrights),
                    new SqlParameter("@SecurityRoleName",item.SecurityRoleName)

                    };
                uow.ExecuteReaderSingleDS<GetSecurityRoleDetailsVM>("AddSecurityRoleDetails", sqlParameters);
                count++;
            }

            return true;
        }

        [HttpPost]
        public Response Changeusertype(string userid)
        {
            Response response = new Response();
            SqlParameter[] sqlParameters =
            {
                    new SqlParameter("@userid",userid),


                    };
            uow.ExecuteReaderSingleDS<GetSecurityRoleDetailsVM>("Sp_UpdateTestUserType", sqlParameters);
            response.Message = "Update";
            response.Status = ResponseStatus.OK;
            return response;
        }
        [HttpPost]
        public Response GetUserByFacebookId([FromBody] UserViewModels.SocialAccountVm socialAccountVm)
        {
            Response response;
            try
            {
                if (string.IsNullOrEmpty(socialAccountVm.FacebookId))
                {
                    response = new Response
                    {
                        Message = "Facebool login failed",
                        ResultData = null,
                        Status = ResponseStatus.Error
                    };
                }
                else
                {
                    SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@FacebookId",socialAccountVm.FacebookId),
                        new SqlParameter("@RoleId",(int)socialAccountVm.Role),
                        new SqlParameter("@email",socialAccountVm.Email)
                    };
                    Exc.AddErrorLog(sqlParameters.ToString());
                    FacebookLoginSPClass userId = uow.ExecuteReaderSingleDS<FacebookLoginSPClass>("GetUserIdBasedonFacebookId", sqlParameters)?.FirstOrDefault();
                    if (string.IsNullOrEmpty(userId?.UserId))
                    {
                        response = new Response
                        {
                            Message = "You can create new account",
                            ResultData = null,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response
                        {
                            Status = ResponseStatus.Restrected,
                            Message = "Already registered.",
                            ResultData = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response = new Response
                {
                    Message = "You can create new account",
                    ResultData = null,
                    Status = ResponseStatus.OK
                };
            }

            return response;
        }

        public Response GetUserByAppleId(string appleId, Role role)
        {
            Response response;
            try
            {
                if (string.IsNullOrEmpty(appleId))
                {
                    response = new Response
                    {
                        Message = "Apple login failed",
                        ResultData = null,
                        Status = ResponseStatus.Error
                    };
                }
                else
                {
                    SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@AppleId",appleId),
                        new SqlParameter("@RoleId",(int)role)
                    };

                    FacebookLoginSPClass sPClass = uow.ExecuteReaderSingleDS<FacebookLoginSPClass>("GetUserIdBasedonAppleId", sqlParameters)?.FirstOrDefault();

                    if (string.IsNullOrEmpty(sPClass?.UserId))
                    {
                        response = new Response
                        {
                            Message = "You can create new account",
                            ResultData = null,
                            Status = ResponseStatus.OK
                        };
                    }
                    else
                    {
                        response = new Response
                        {
                            Status = ResponseStatus.Restrected,
                            Message = "Already registered.",
                            ResultData = sPClass.Email
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response
                {
                    Message = "You can create new account",
                    ResultData = null,
                    Status = ResponseStatus.OK
                };
            }

            return response;
        }

        private async Task<FbTokenInfo> GetTokenInfoAsync(string facebookToken)
        {
            FbTokenInfo fbTokenInfo = null;

            if (!string.IsNullOrWhiteSpace(facebookToken))
            {
                try
                {
                    var accesstokenResponse = await GetFbAccessTokenAsync();

                    fbTokenInfo = JsonConvert.DeserializeObject<FbTokenInfo>(
                        await httpClient.GetAsync($"{FacebookConstants.ApiBaseUrl}{FacebookConstants.DebugToken}".Replace("{{inputToken}}", facebookToken).Replace("{{accessToken}}", accesstokenResponse.AccessToken))
                    );

                    fbTokenInfo = fbTokenInfo.Data.AppId == FacebookConstants.AppId && fbTokenInfo.Data.IsValid ? fbTokenInfo : null;
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                }
            }

            return fbTokenInfo;
        }
        #endregion

        #region Helper Methods
        [HttpGet]
        public AspNetUsers GetUserByEmailOrPhoneNumber(string email, string phoneNumber, string role)
        {
            bool userAvailable = true;
            int index = 0;
            var applicationUsers = uow.Repository<AspNetUsers>().GetAll().Where(u => (!string.IsNullOrWhiteSpace(phoneNumber) && u.PhoneNumber == phoneNumber) || (!string.IsNullOrWhiteSpace(email) && u.Email == email)).ToList();
            for (int i = 0; i < applicationUsers.Count(); i++)
            {
                index = i;
                string existingRole = GetRoleByUserId(applicationUsers[i].Id);
                if (existingRole == role)
                {
                    userAvailable = false;
                }
            }

            if (userAvailable)
            {
                return null;

            }
            else
            {
                return applicationUsers[index];
            }
        }
        [HttpGet]
        public AspNetUsers GetUserByID(string Id)
        {
            var aspNetUser = uow.Repository<AspNetUsers>().GetAll().Where(a => a.Id == Id).FirstOrDefault();
            return aspNetUser;
        }

        public string GetRoleByUserId(string userId)
        {
            string role = "";
            try
            {
                //ApplicationUser applicationUser = _userManager..Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber || u.Email == email);
                var roleId = _identityContext.AspNetUserRoles.Where(e => e.UserId == userId).Select(e => e.RoleId).FirstOrDefault();
                switch (roleId)
                {
                    case "1":
                        role = "Tradesman";
                        break;
                    case "2":
                        role = "Organization";
                        break;
                    case "3":
                        role = "Customer";
                        break;
                    case "4":
                        role = "Supplier";
                        break;
                    case "5":
                        role = "Admin";
                        break;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return role;
        }
        #endregion

        [HttpPost]
        public string AdminUserLoging([FromBody] LoginVM model)
        {
            List<Response> response = new List<Response>();
            SqlParameter[] sqlParameters =
            {
                    new SqlParameter("@EmailOrPhoneNumber",model.EmailOrPhoneNumber),
                    new SqlParameter("@Browser",model.Browser),
                    new SqlParameter("@IpAddress",model.IpAddress),
                    new SqlParameter("@Role",model.Role),
                    new SqlParameter("@UserId",model.UserId),
                    new SqlParameter("@FirebaseClientId",model.FirebaseClientId),
                    new SqlParameter("@RequestType",model.RequestType)

                    };
            response = uow.ExecuteReaderSingleDS<Response>("Sp_LogingAdminUser", sqlParameters);

            return response[0].Message;
        }

        [HttpGet]
        public bool GetUserBlockStatus(string userId)
        {
            try
            {
                ApplicationUser isBlocked = new ApplicationUser();
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    isBlocked = _netUserManager.Users.Where(x => x.Id == userId).FirstOrDefault();
                }
                return isBlocked.IsBlocked;

            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return false;
            }
        }

        [HttpGet]
        public Response GetUserPinStatus(string role, string emailOrPhone)
        {

            Response response = new Response();
            GetUserPinStatusVM getUserPinStatus = new GetUserPinStatusVM();

            if (!string.IsNullOrEmpty(emailOrPhone))
            {
                try
                {
                    int roleId = (int)(Role)Enum.Parse(typeof(Role), role);

                    SqlParameter[] sqlParameters = {
                    new SqlParameter("@userName",emailOrPhone),
                    new SqlParameter("@role", roleId)
                    };
                    getUserPinStatus = uow.ExecuteReaderSingleDS<GetUserPinStatusVM>("GetUserPinStatus", sqlParameters).FirstOrDefault();
                    if (getUserPinStatus != null)
                    {
                        response.Status = ResponseStatus.OK;
                        response.Message = getUserPinStatus.userId;
                        response.ResultData = getUserPinStatus?.HasPIN ?? false;
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "User not found";
                    }
                }
                catch (Exception ex)
                {
                    Exc.AddErrorLog(ex);
                    response.Message = ex.Message;
                    response.Status = ResponseStatus.Error;
                }


            }
            else
            {
                response.Message = "emailOrPhone is not found";
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        [HttpPost]
        public Response DeleteUserInfo([FromBody] DeleteUserInfoVM deleteUserInfoVM)
        {

            Response response = new Response();

            try
            {
                SqlParameter[] sqlParameters =
                {
                            new SqlParameter("@userId ", deleteUserInfoVM.userId),
                            new SqlParameter("@deletedBy ", deleteUserInfoVM.deletedBy),
                    };
                var result = uow.ExecuteReaderSingleDS<Response>("deleteuser", sqlParameters);
                string message = result.Select(x => x.Message).FirstOrDefault();
                response.Message = message;
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
                return response;
            }
        }

        [HttpGet]
        public List<AllUsersVM> GetAllActiveUsers()
        {
            List<AllUsersVM> list = new List<AllUsersVM>();

            SqlParameter[] sqlParameters =
            {};
            list = uow.ExecuteReaderSingleDS<AllUsersVM>("Sp_GetAllUsersWithUserTypeId", sqlParameters);

            return list;
        }

        [HttpPost]
        public List<CustomersDTO> GetDeleteUserInfo([FromBody] DeleteUserInfoVM deleteUserInfoVM)
        {

            List<CustomersDTO> list = new List<CustomersDTO>();

            try
            {
                SqlParameter[] sqlParameters =
                {
                            new SqlParameter("@Email", deleteUserInfoVM.UserName),
                            new SqlParameter("@MobileNo", deleteUserInfoVM.Phone),
                            new SqlParameter("@UserType",deleteUserInfoVM.UserRole),
                            new SqlParameter("@UserId ", deleteUserInfoVM.userId),
                    };
                list = uow.ExecuteReaderSingleDS<CustomersDTO>("Sp_GetUserByPhoneNoOrUserId", sqlParameters);

                return list;
            }
            catch (Exception ex)
            {
                return new List<CustomersDTO>();

            }
        }


        public Response BlockUser(string userId, bool status)
        {

            Response response = new Response();

            try
            {
                if (status)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
                SqlParameter[] sqlParameters =
                  {

                    new SqlParameter("@UserId",userId),
                    new SqlParameter("@status",status)
                };

                 uow.ExecuteReaderSingleDS<Response>("Sp_BlockUser", sqlParameters);
                response.Message = "User Has Been Blocked Successfully !!!";
                response.Status = ResponseStatus.OK;
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;

        }
        [HttpPost]
        public Response AddUpdateMenuItem([FromBody] SiteMenuVM siteMenuVM)
        {

            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@MenuId",siteMenuVM.MenuId),
                    new SqlParameter("@MenuItemName",siteMenuVM.MenuItemName),
                    new SqlParameter("@UserId",siteMenuVM.UserId),
                    new SqlParameter("@Active",siteMenuVM.Active),
                    new SqlParameter("@IconName",siteMenuVM.IconName),
               };
                response = uow.ExecuteReaderSingleDS<Response>("SP_AddUpdateMenuItem", sqlParameters).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;

        }
        public List<SiteMenuVM> GetMenuItemsList()
        {
            List<SiteMenuVM> response = new List<SiteMenuVM>();
            try
            {
                SqlParameter[] sqlParameters = { };
                response = uow.ExecuteReaderSingleDS<SiteMenuVM>("SP_GetMenuItemsList", sqlParameters).ToList();
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SiteMenuVM>();
            }
        }
        [HttpPost]
        public Response AddUpdateSubMenuItem([FromBody] SiteMenuVM siteMenuVM)
        {
            Response response = new Response();
            try
            {
                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@MenuId",siteMenuVM.MenuId),
                    new SqlParameter("@SubMenuId",siteMenuVM.SubMenuId),
                    new SqlParameter("@SubMenuItemName",siteMenuVM.SubMenuItemName),
                    new SqlParameter("@UserId",siteMenuVM.UserId),
                    new SqlParameter("@Active",siteMenuVM.Active),
               };
                response = uow.ExecuteReaderSingleDS<Response>("SP_AddUpdateSubMenuItem", sqlParameters).FirstOrDefault();
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ResultData = null;
                response.Status = ResponseStatus.Error;

                Exc.AddErrorLog(ex);
            }

            return response;

        }
        public List<SiteMenuVM> GetSubMenuItemsList()
        {
            List<SiteMenuVM> response = new List<SiteMenuVM>();
            try
            {
                SqlParameter[] sqlParameters = { };
                response = uow.ExecuteReaderSingleDS<SiteMenuVM>("SP_GetSubMenuItemsList", sqlParameters).ToList();
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<SiteMenuVM>();
            }
        }

        public Response UpdateLastActiveLogin(string userId)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrWhiteSpace(userId))
                {

                    SqlParameter[] sqlParameters = {

                    new SqlParameter("@UserId",userId)

                    };
                    uow.ExecuteReaderSingleDS<Response>("Sp_UpdateLastActiveLogin", sqlParameters);
                    response.Status = ResponseStatus.OK;
                    response.Message = "User Last Active Login (Date & Time) has been Updated Successfully !!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }
            return response;
        }
    
        public GetAccountsTypeVM GetSupplierAcountsType(string userId)
        {
            try
            {
                GetAccountsTypeVM getAccountsTypeVM = new GetAccountsTypeVM();

                SqlParameter[] sqlParameter = {
                new SqlParameter("userId",userId)
            };
                getAccountsTypeVM = uow.ExecuteReaderSingleDS<GetAccountsTypeVM>("Sp_GetSupplierAcountsType", sqlParameter).FirstOrDefault();

                return getAccountsTypeVM;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new GetAccountsTypeVM();
            }
        }
    }
}

