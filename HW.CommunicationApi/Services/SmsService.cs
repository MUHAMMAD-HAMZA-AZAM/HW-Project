using HW.CommunicationApi.Code;
using HW.CommunicationModels;
using HW.CommunicationViewModels;
using HW.Http;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZongApi;

namespace HW.CommunicationApi.Services
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(SmsVM smsVM);
        Task<bool> SendRegisterUserSms(SmsVM smsVM);
        Task<Response> PostRequestCallBack(CallRequest callRequestModel);
    }
    public class SmsService : ISmsService
    {
        private readonly IHttpClientService httpClient;
        private readonly UfoneSmsApiConfig ufoneSmsApiConfig;
        private readonly ZongSmsApiConfig zongSmsApiConfig;
        private readonly GenericAppConfig genericAppConfig;
        private readonly JazzSmsApiConfig jazzSmsApiConfig;
        private readonly ICorporateCBS zongApi;
        private readonly IExceptionService Exc;
        private readonly IUnitOfWork uow;
        private string Error1, Error2 = string.Empty;
        private int Retries = 0;
        public SmsService
        (
            IHttpClientService httpClient,
            UfoneSmsApiConfig ufoneSmsApiConfig,
            GenericAppConfig genericAppConfig,
            ZongSmsApiConfig zongSmsApiConfig,
            JazzSmsApiConfig jazzSmsApiConfig,
            ICorporateCBS zongApi,
            IExceptionService Exc,
            IUnitOfWork unitOfWork
        )
        {
            this.httpClient = httpClient;
            this.ufoneSmsApiConfig = ufoneSmsApiConfig;
            this.zongSmsApiConfig = zongSmsApiConfig;
            this.genericAppConfig = genericAppConfig;
            this.jazzSmsApiConfig = jazzSmsApiConfig;
            this.zongApi = zongApi;
            this.Exc = Exc;
            this.uow = unitOfWork;
        }

        public async Task<bool> SendSmsAsync(SmsVM smsVM)
        {
            long smsId = 0;
            try
            {
                List<string> listOfNumber = new List<string>();
                foreach (var item in smsVM.MobileNumberList)
                {
                    listOfNumber.Add($"92{item.Substring(1)}");
                }
                string stringOfPhoneNumber = string.Join(",", listOfNumber.ToArray());

                string mobileNumberFormatted = smsVM.MobileNumber != null ? $"92{smsVM.MobileNumber.Substring(1)},{923179994531}" : stringOfPhoneNumber;

                smsId = SaveOtp(smsVM);

                if (genericAppConfig.PreferredSmsApi == "ufone")
                {
                    string urlWithQueryString =
                        $"{ufoneSmsApiConfig.Url}?id={ufoneSmsApiConfig.Id}&message={smsVM.Message}&shortcode={ufoneSmsApiConfig.Shortcode}&lang={ufoneSmsApiConfig.Lang}" +
                        $"&mobilenum={mobileNumberFormatted}&password={ufoneSmsApiConfig.Password}";
                    var result = await httpClient.GetAsync(urlWithQueryString);
                }
                else if (genericAppConfig.PreferredSmsApi == "zong")
                {
                    QuickSMSResquest quickSMSResquest = new QuickSMSResquest()
                    {
                        loginId = zongSmsApiConfig.LoginId,
                        loginPassword = zongSmsApiConfig.LoginPassword,
                        Mask = zongSmsApiConfig.Mask,
                        ShortCodePrefered = zongSmsApiConfig.ShortCodePrefered,
                        UniCode = zongSmsApiConfig.UniCode,

                        Destination = mobileNumberFormatted,
                        Message = smsVM.Message
                    };

                    string message = zongApi.QuickSMSAsync(quickSMSResquest).Result;
                }
                else if (genericAppConfig.PreferredSmsApi == "jazz")
                {
                    string result = "";

                    string message = HttpUtility.UrlEncode(smsVM.Message);

                    string strPost = $"id={jazzSmsApiConfig.Id}&pass={jazzSmsApiConfig.Password}&msg={message}&to={mobileNumberFormatted}&mask={jazzSmsApiConfig.Mask}&type={jazzSmsApiConfig.Type}&lang={jazzSmsApiConfig.Lang}";

                    StreamWriter myWriter = null;

                    HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://www.opencodes.pk/api/medver.php/sendsms/url");
                    objRequest.Method = "POST";
                    objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
                    objRequest.ContentType = "application/x-www-form-urlencoded";

                    try
                    {
                        myWriter = new StreamWriter(objRequest.GetRequestStream()); myWriter.Write(strPost);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }
                    finally
                    {
                        myWriter.Close();
                    }

                    HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

                    using (StreamReader streamReader = new StreamReader(objResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                        Exc.AddErrorLog(result + "Mobile Number is " + mobileNumberFormatted);
                    };

                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return false;
        }

        public async Task<bool> SendRegisterUserSms(SmsVM smsVM)
        {
            long smsId = 0;
            try
            {
                List<string> listOfNumber = new List<string>();
                foreach (var item in smsVM.MobileNumberList)
                {
                    listOfNumber.Add($"92{item.Substring(1)}");
                }
                string stringOfPhoneNumber = string.Join(",", listOfNumber.ToArray());

                string mobileNumberFormatted = smsVM.MobileNumber != null ? $"92{smsVM.MobileNumber.Substring(1)},{923179994531}" : stringOfPhoneNumber;

                smsId = SaveOtp(smsVM);

                    if (genericAppConfig.PreferredSmsApi == "ufone")
                    {
                        string urlWithQueryString =
                            $"{ufoneSmsApiConfig.Url}?id={ufoneSmsApiConfig.Id}&message={smsVM.Message}&shortcode={ufoneSmsApiConfig.Shortcode}&lang={ufoneSmsApiConfig.Lang}" +
                            $"&mobilenum={mobileNumberFormatted}&password={ufoneSmsApiConfig.Password}";
                        var result = await httpClient.GetAsync(urlWithQueryString);
                    }
                    else if (genericAppConfig.PreferredSmsApi == "zong")
                    {
                        QuickSMSResquest quickSMSResquest = new QuickSMSResquest()
                        {
                            loginId = zongSmsApiConfig.LoginId,
                            loginPassword = zongSmsApiConfig.LoginPassword,
                            Mask = zongSmsApiConfig.Mask,
                            ShortCodePrefered = zongSmsApiConfig.ShortCodePrefered,
                            UniCode = zongSmsApiConfig.UniCode,

                            Destination = mobileNumberFormatted,
                            Message = smsVM.Message
                        };

                        string message = zongApi.QuickSMSAsync(quickSMSResquest).Result;
                    }
                    else if (genericAppConfig.PreferredSmsApi == "jazz")
                    {
                        string result = "";

                        string message = HttpUtility.UrlEncode(smsVM.Message);

                        string strPost = $"id={jazzSmsApiConfig.Id}&pass={jazzSmsApiConfig.Password}&msg={message}&to={mobileNumberFormatted}&mask={jazzSmsApiConfig.Mask}&type={jazzSmsApiConfig.Type}&lang={jazzSmsApiConfig.Lang}";

                        StreamWriter myWriter = null;

                        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("http://www.opencodes.pk/api/medver.php/sendsms/url");
                        objRequest.Method = "POST";
                        objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
                        objRequest.ContentType = "application/x-www-form-urlencoded";

                        try
                        {
                            myWriter = new StreamWriter(objRequest.GetRequestStream()); myWriter.Write(strPost);
                        }
                        catch (Exception e)
                        {
                            Console.Write(e.Message);
                        }
                        finally
                        {
                            myWriter.Close();
                        }

                        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

                        using (StreamReader streamReader = new StreamReader(objResponse.GetResponseStream()))
                        {
                            result = streamReader.ReadToEnd();
                            Exc.AddErrorLog(result + "Mobile Number is " + mobileNumberFormatted);
                        };

                      
                    }
                    return true;
                   
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return false;
        }

        public long SaveOtp(SmsVM smsVM)
        {
            try
            {
                Otpsms otpsms = new Otpsms();

                otpsms.IsSent = false;
                otpsms.Message = smsVM.Message;
                otpsms.RecieverMobileNumber = smsVM.MobileNumber;
                otpsms.Retries = 0;
                otpsms.CreatedOn = DateTime.Now;
                otpsms.SentOn = DateTime.Now;

                uow.Repository<Otpsms>().Add(otpsms);
                uow.Save();
                return otpsms.SmsId;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return 0;
            }
        }

        public void UpdateSentSuccessfully(long smsId)
        {
            try
            {
                var otpSms = uow.Repository<Otpsms>().GetById(smsId);
                otpSms.IsSent = true;
                otpSms.CreatedBy = Error1 + " " + Error2;
                uow.Repository<Otpsms>().Update(otpSms);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public void UpdateSentFailure(long smsId)
        {
            try
            {
                var otpSms = uow.Repository<Otpsms>().GetById(smsId);
                otpSms.Retries = Retries;
                otpSms.CreatedBy = Error1 + " " + Error2;
                //otpSms.Retries = otpSms.Retries++;
                uow.Repository<Otpsms>().Update(otpSms);
                uow.Save();
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
            }
        }

        public async Task<Response> PostRequestCallBack(CallRequest callRequestModel)
        {
            Response response = new Response();
            try
            {
                await uow.Repository<CallRequest>().AddAsync(callRequestModel);
                await uow.SaveAsync();
                response.Message = "Our team has been notified.Hoomwork will contact you soon";
                response.Status = ResponseStatus.OK;
                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return new Response();
            }

        }
    }
}
