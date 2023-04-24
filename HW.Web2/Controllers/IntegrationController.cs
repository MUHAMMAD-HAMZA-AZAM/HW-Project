using HW.PackagesAndPaymentsViewModels;
using HW.Utility;
using HW.Utility.Constants;
using HW.Web2.Code;
using HW.Web2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HW.Web2.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class IntegrationController : Controller
    {
        private readonly IIntegrationService _integrationService;
        private readonly IExceptionService _loggingService;
        //private readonly WebApiConfig _apiConfig;
        public IntegrationController(IIntegrationService integrationService, IExceptionService loggingService)
        {
            _integrationService = integrationService;
            _loggingService = loggingService;
            //_apiConfig = apiConfig;
        }
        //[HttpGet("[action]")]
        public string Start()
        {
            return "Web Controller is started.";
        }

        [HttpPost("[action]")]
        public async Task<Response> ProceedToJazzCash([FromBody] JazzCashPaymentDetailVM objPayment)
        {
            var objResponse = new Response();
            try
            {
                var token = string.Empty;
                string TokenForJazzCash = string.Empty;
                string SecureHash = string.Empty;
                TokenForJazzCash = GetJazzCashSecureToken();
                objPayment.ppmpf_1 = TokenForJazzCash;
                var dic = ConvertToDictionary(objPayment);
                dic.Remove("Id");
                dic.Remove("UserId");
                SecureHash = GenerateHash(dic);

                objPayment.pp_SecureHash = SecureHash;
                _loggingService.AddErrorLog(new Exception("maintaining session before sending to jazz cash" + JsonConvert.SerializeObject(objPayment)));
                if (Request.Headers["Authorization"].FirstOrDefault() != null)
                {
                    token = Request.Headers["Authorization"].ToString().Substring(7);
                }
                var gtwyUrl = "https://www.hoomwork.com/GatewayNet5/";
                _loggingService.AddErrorLog(new Exception("ProceedToJazzCash Url: " + $"{gtwyUrl}{ApiRoutes.Payment.JazzCashCallBack}"));

                _loggingService.AddErrorLog(new Exception("token: " + token));

                await _integrationService.ProceedToJazzCash(objPayment, token);
                objResponse.ResultData = objPayment;
                objResponse.Status = ResponseStatus.OK;
            }
            catch (Exception ex)
            {
                _loggingService.AddErrorLog("Exeption object:" + ex);
                objResponse.Status = ResponseStatus.Error;
            }
            return objResponse;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> JazzCashCallBack(IFormCollection formCollection)
        {
            try
            {
                Dictionary<string, string> callBackData = new Dictionary<string, string>();
                string ReceivedSecureHash = string.Empty;
                foreach (var _key in formCollection.Keys)
                {
                    callBackData.Add(_key, formCollection[_key]);
                }
                JazzCashPaymentDetailVM detailVM = new JazzCashPaymentDetailVM();
                var json = JsonConvert.SerializeObject(callBackData, Newtonsoft.Json.Formatting.Indented);
                detailVM = JsonConvert.DeserializeObject<JazzCashPaymentDetailVM>(json);
                callBackData.Remove("pp_SecureHash");
                ReceivedSecureHash = detailVM.pp_SecureHash;
                //Get from Database
                var saveData = JsonConvert.DeserializeObject<JazzCashPaymentDetailVM>(await _integrationService.GetJazzCashMerchantDetails(detailVM.ppmpf_1));
                _loggingService.AddErrorLog(new Exception("try Response from Jazz Cash " + JsonConvert.SerializeObject(detailVM)));

                if (detailVM.pp_ResponseCode == "000")
                {
                    var RegeneratedHash = GenerateHash(callBackData);
                    _loggingService.AddErrorLog(new Exception("Regenerated Hash " + RegeneratedHash));
                    //check from database
                    //if (ReceivedSecureHash.ToLower() == RegeneratedHash.ToLower() && saveData.ppmpf_1 == detailVM.ppmpf_1)
                    if (saveData.ppmpf_1 == detailVM.ppmpf_1)
                    {
                        if (formCollection["ppmpf_2"] == "supplier")
                        {
                            //For Supplier
                            await _integrationService.JazzCashCallBack(detailVM);
                        }
                        else
                        {
                            await _integrationService.JazzCashCallBack(detailVM);
                        }
                    }
                    else
                    {
                        detailVM.pp_ResponseCode = "123";
                        detailVM.pp_ResponseMessage = "mismatched, marked it suspicious or reject it";
                    }
                }
                //return Redirect($"http://localhost:4200/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={detailVM.pp_BillReference}");
                //return Redirect($"https://www.hoomwork.com/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={detailVM.pp_BillReference}&pp_ResponseMessage={detailVM.pp_ResponseMessage}");
                if (formCollection["ppmpf_2"] == "supplier")
                {
                    return Redirect($"https://mall.hoomwork.com/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={detailVM.pp_BillReference}&pp_ResponseMessage={detailVM.pp_ResponseMessage}");

                }
                else
                {
                    return Redirect($"https://www.hoomwork.com/Message/SuccessMessage?pp_ResponseCode={detailVM.pp_ResponseCode}&pp_BillReference={detailVM.pp_BillReference}&pp_ResponseMessage={detailVM.pp_ResponseMessage}");
                }
            }
            catch (Exception ex)
            {
                _loggingService.AddErrorLog("Exeption object:" + ex);
                _loggingService.AddErrorLog("catch Jazz cash (from object method)  test (object): " + JsonConvert.SerializeObject(formCollection));
                return NotFound();
            }
        }
        private string GetJazzCashSecureToken()
        {
            var key = SessionKey.JazzCashTokenKey;
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(key, 8)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            return resultToken.ToString();
        }
        private string GenerateHash(Dictionary<string, string> dictionary)
        {
            string hashString = "173wxy1s22";
            foreach (var pair in dictionary.OrderBy(p => p.Key))
            {
                if (pair.Value != null && pair.Value != "") {
                    hashString += '&' + pair.Value;
                }
            }
            //hashString = hashString.Remove(hashString.Length - 1);
            
            //Generate Hash
            var key = Encoding.ASCII.GetBytes("173wxy1s22");
            var provider = new System.Security.Cryptography.HMACSHA256(key);
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(hashString));
            var secureHash = "";
            for (var i = 0; i < hash.Length; i++)
            {
                secureHash += hash[i].ToString("x2");
            }
            return secureHash;
        }

        public Dictionary<string,string> ConvertToDictionary(JazzCashPaymentDetailVM txnDetails) {
            Dictionary<string, string> jazzCashData = new Dictionary<string, string>();
            PropertyInfo[] infos = txnDetails.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                jazzCashData.Add(info.Name, Convert.ToString(info.GetValue(txnDetails, null)));
            }
            return jazzCashData;
        }

    }
}
