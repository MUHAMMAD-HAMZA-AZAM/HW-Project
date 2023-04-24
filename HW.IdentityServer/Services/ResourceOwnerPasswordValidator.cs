using HW.CallModels;
using HW.IdentityServerModels;
using HW.IdentityViewModels;
using HW.Utility;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HW.IdentityServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IExceptionService Exc;
        private readonly IUnitOfWork uow;
        private readonly PasswordHasherCompatibilityMode _compatibilityMode;
        private readonly int _iterCount;
        private readonly RandomNumberGenerator _rng;

        public ResourceOwnerPasswordValidator(IUnitOfWork unitOfWork, IExceptionService Exc)
        {
            uow = unitOfWork;
            this.Exc = Exc;
        }
        //Authorize User 
        #region Password Verification
        public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }
            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // read the format marker from the hashed password
            if (decodedHashedPassword.Length == 0)
            {
                return PasswordVerificationResult.Failed;
            }
            switch (decodedHashedPassword[0])
            {
                case 0x00:
                    if (VerifyHashedPasswordV2(decodedHashedPassword, providedPassword))
                    {
                        // This is an old password hash format - the caller needs to rehash if we're not running in an older compat mode.
                        return (_compatibilityMode == PasswordHasherCompatibilityMode.IdentityV3)
                            ? PasswordVerificationResult.SuccessRehashNeeded
                            : PasswordVerificationResult.Success;
                    }
                    else
                    {
                        return PasswordVerificationResult.Failed;
                    }

                case 0x01:
                    int embeddedIterCount;
                    if (VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, out embeddedIterCount))
                    {
                        // If this hasher was configured with a higher iteration count, change the entry now.
                        return (embeddedIterCount < _iterCount)
                            ? PasswordVerificationResult.SuccessRehashNeeded
                            : PasswordVerificationResult.Success;
                    }
                    else
                    {
                        return PasswordVerificationResult.Failed;
                    }

                default:
                    return PasswordVerificationResult.Failed; // unknown format marker
            }
        }
        private static bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // We know ahead of time the exact length of a valid hashed password payload.
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }

        private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password, out int iterCount)
        {
            iterCount = default(int);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = hashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch (Exception ex)
            {
                //Exc.AddErrorLog(ex);
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        #endregion
        //Validat User
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            try
            {
                var emailorphonenumber = context.UserName;
                var firebaseClientId = context.Request.Raw["firebaseClientId"];
                staticResources.FirebaseClientId = context.Request.Raw["firebaseClientId"];
                staticResources.Role = context.Request.Raw["role"];
                staticResources.UserId = context.Request.Raw["userId"];
                staticResources.EntityId = context.Request.Raw["entityId"];

                
                var aspNetUsers = uow.Repository<AspNetUsers>().Get(x => x.Id == staticResources.UserId).FirstOrDefault();

                if (aspNetUsers != null && !string.IsNullOrEmpty(staticResources.UserId))
                {

                    var check = VerifyHashedPassword(aspNetUsers?.PasswordHash, context.Password);
                    if (check == PasswordVerificationResult.Success)
                    {
                        if (!string.IsNullOrWhiteSpace(firebaseClientId))
                        {
                            aspNetUsers.FirebaseClientId = firebaseClientId;
                            uow.Repository<AspNetUsers>().Update(aspNetUsers);
                            uow.Save();
                        }
                        context.Result = new GrantValidationResult(
                        subject: emailorphonenumber,
                        authenticationMethod: "custom"
                        );
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                        Exc.AddErrorLog(new Exception(TokenRequestErrors.InvalidGrant + ""));
                    }
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                    Exc.AddErrorLog(new Exception(TokenRequestErrors.InvalidGrant + ""));
                }
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
            return;
        }

    }





}
