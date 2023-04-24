using HW.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.TradesmanApi.Controllers
{
    public class BaseController : Controller
    {
        #region Helpers

        public List<ErrorModel> MapErrorsFromModelState()
        {
            List<ErrorModel> errors = new List<ErrorModel>();

            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateValues = ModelState[modelStateKey];
                string errorsString = String.Join(",", modelStateValues.Errors.Select(e => e.ErrorMessage));

                errors.Add(new ErrorModel { Key = modelStateKey, Description = errorsString });

            }

            return errors;
        }

        #endregion
    }
}
