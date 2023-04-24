using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.AuthO
{
    public static class PermissionType
    {
        public const string Accesss = "Access";
        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string Delete = "Delete";
        public const string Print = "Print";
        public const string Export = "Export";
    }

    public enum ErrorCode
    {
        Unauthorized = 701,
        SessionInvalid = 702,
        TokenExpired = 703,
        InvalidAudience = 704,
        Invalidalgorithm = 705,
    }

    public static class ClaimType
    {
        public const string Algo = "alg";
    }

    public static class Roles
    {
        public const string Customer = "Customer";
        public const string Tradesman = "Tradesman";
        public const string Supplier = "Supplier";
        public const string Admin = "Admin";
    }

    public static class ClaimValue
    {
        public const string Algo = "RS256";
    }

    public class TokenHeader
    {
        public string alg { get; set; }
        public string kid { get; set; }
        public string typ { get; set; }
        public string x5t { get; set; }
    }

    public class CustomError
    {
        public string Error { get; }
        public ErrorCode Code { get; }

        public CustomError(string message, ErrorCode errorCode)
        {
            Error = message;
            Code = errorCode;
        }
    }
}
