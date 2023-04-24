using HW.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW.IdentityViewModels
{
    public static class PasswordValidator
    {
        public static string ErrorMsg
        {
            get
            {
                List<string> errors = new List<string>();
                errors.Add("Password must have:");
                errors.Add($"at least {PasswordRules.RequiredLength} characters");

                if (PasswordRules.RequireNonAlphanumeric)
                {
                    errors.Add($"at least one special character (e.g. !@#$%^&*)");
                }
                if (PasswordRules.RequireDigit)
                {
                    errors.Add($"at least one digit (0-9)");

                }
                if (PasswordRules.RequireLowercase)
                {
                    errors.Add($"at least one lower case character (a-z)");
                }
                if (PasswordRules.RequireUppercase)
                {
                    errors.Add($"at least one upper case character (A-Z)");
                }

                return String.Join("\n", errors);
            }
        }

        public static bool BeValid(string password)
        {
            bool isValid = true;

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(password) || password.Length < PasswordRules.RequiredLength)
            {
                isValid = false;
                //errors.Add(String.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength));
            }
            else if (PasswordRules.RequireNonAlphanumeric && password.All(IsLetterOrDigit))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireNonLetterOrDigit);
            }
            else if (PasswordRules.RequireDigit && password.All(c => !IsDigit(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireDigit);
            }
            else if (PasswordRules.RequireLowercase && password.All(c => !IsLower(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireLower);
            }
            else if (PasswordRules.RequireUppercase && password.All(c => !IsUpper(c)))
            {
                isValid = false;
                //errors.Add(Resources.PasswordRequireUpper);
            }

            return isValid;
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private static bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private static bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }

}
