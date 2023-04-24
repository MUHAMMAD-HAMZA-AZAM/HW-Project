using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HW.Utility
{
    public class Common
    {
        #region Static Properties

        #endregion

        #region Static Methods
        public static List<T> MapToList<T>(DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(x => x.Name.ToUpper(), x => x);
                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value ? null : val), null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }
        #endregion
    }

    public class Response
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object ResultData { 
            get; set; }
    }
    //public enum Status
    //{
    //    Success = HttpStatusCode.OK,
    //    Failure = HttpStatusCode.InternalServerError,
    //    Restricted = HttpStatusCode.Forbidden,
    //    PartialContent = HttpStatusCode.PartialContent,
    //    Conflict = HttpStatusCode.Conflict
    //}
    public class AppUtil
    {
        public const string SelectedLanguage = "SelectedLanguage";
        public const string Language = "Language";
        public const string OnRotateLanguage = "OnRotateLanguage";
    }

    public class LogModel
    {
        public LogLevel LogLevel { get; set; }
        public LoggingEvents LoggingEvent { get; set; }
        public string Message { get; set; }
        public ExceptionModel Exception { get; set; }
    }

    public class ExceptionModel
    {
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public Exception InnerException { get; set; }
        public int HResult { get; set; }
        public string HelpLink { get; set; }
    }
    public class IdValueVM
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
    public class IdTextVM
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }

    public class IdValueCategoryVM
    {
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
    public class IdValuePriceVM
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long SkillId { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? VisitCharges { get; set; }
    }
    public class SubSkillWithSkillVM
    {
        public long SubSkillId { get; set; }
        public string SubSkillName { get; set; }
        public string SkillName { get; set; }
        public long SkillId { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? VisitCharges { get; set; }
    }
    public class IdentityIdsValueVM
    {
        public string UserId { get; set; }
        public string FirebaseId { get; set; }
    }

    public class IdentityUserTypeVM
    {
        public string UserId { get; set; }
        public bool IsTestUser { get; set; }
    }

    public class IdValueImageVM
    {
        public long Id { get; set; }
        public byte[] Value { get; set; }
        public bool IsMain { get; set; }
    }

    public class ErrorModel
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }

    public static class CustomRegularExpressions
    {
        static string emailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        public static string mobileNumberPattern = @"^[0][1-9]\d{9}$";
        static string alphabetsOnlyPattern = @"^[A-Za-z]+$";
        public static string specialCharsPattern = @"[^a-zA-Z]+";
        static string cnicPattern = @"^\d{13}$";
        public static string onlynumeric = @"^[0-9]+$";
       // public static string alphabetWithSpacesPattern = @"^[a-z A-Z \d-]+$";
        public static string alphabetWithSpacesPattern = @"^[a-zA-Z \-]+$";


        public static Regex Email()
        {
            return new Regex(emailPattern, (RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture));
        }

        public static Regex MobileNumber()
        {
            return new Regex(mobileNumberPattern);
        }

        public static Regex NumericOnly()
        {
            return new Regex(onlynumeric);
        }

        public static Regex AlphabetsOnly()
        {
            return new Regex(alphabetsOnlyPattern);
        }
        public static Regex AlphabetsWithSpaces()
        {
            return new Regex(alphabetWithSpacesPattern);
        }
        public static Regex Cnic()
        {
            return new Regex(cnicPattern);
        }
    }

    public static class PasswordRules
    {
        public static int RequiredLength = 9;
        public static bool RequireLowercase = true;
        public static bool RequireUppercase = true;
        public static bool RequireDigit = true;
        public static bool RequireNonAlphanumeric = false;
    }

    public class ImageVMDTO
    {
        public string Name { get; set; }
        public byte[] Value { get; set; }
    }
    
}
