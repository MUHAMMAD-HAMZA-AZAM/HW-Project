//using HW.UserViewModels;
using HW.UserViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace HW.IdentityServer.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork uow;
        IExceptionService Exc;
        public UserService(IUnitOfWork unitOfWork, IExceptionService Exc_)
        {

            uow = unitOfWork;
            Exc = Exc_;
        }


        public List<AspnetUserVM> GetUserListByRoleId(int RoleId)
        {
            try
            {
                SqlParameter[] sqlParameters =
                   {
                    new SqlParameter("@RoleId",RoleId),

                };
                return uow.ExecuteReaderSingleDS<AspnetUserVM>("SP_GET_ASPNETUSERBYROLEID", sqlParameters).ToList();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new List<AspnetUserVM>();
            }
        }
    }
}
