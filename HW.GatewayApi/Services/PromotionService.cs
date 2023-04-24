using HW.GatewayApi.Code;
using HW.Http;
using HW.PackagesAndPaymentsModels;
using HW.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{
    public interface IPromotionService
    {
        Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode);
        Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemption);
        Task<Response> AddEditRedemptions(Redemptions redemptions);
        Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode);
        Task<string> GetRedemptionById(string userId);
        Task<string> GetReferalByRoleId(string userId);
    }
    public class PromotionService: IPromotionService
    {
        private readonly IHttpClientService httpClient;
        private readonly ClientCredentials clientCred;
        private readonly IExceptionService Exc;
        private readonly ApiConfig _apiConfig;

        public PromotionService(IHttpClientService httpClient, ClientCredentials clientCred, IExceptionService Exc, ApiConfig apiConfig)
        {
            this.clientCred = clientCred;
            this.httpClient = httpClient;
            _apiConfig = apiConfig;
            this.Exc = Exc;
        }

        public async Task<Response> AddEditPromotionReferalCode(ReferalCode referalCode)
        {
            Response response = new Response();

            try
            {
                string sharedRefferalUserID = JsonConvert.DeserializeObject<string>
                    (await httpClient.GetAsync($"{_apiConfig.CustomerApiUrl}{ApiRoutes.Customer.GetCustomerByPublicId}?publicID={referalCode.ReferralCode}"));

                if (!string.IsNullOrEmpty(sharedRefferalUserID) && sharedRefferalUserID != referalCode.ReferredUser)
                {
                    List<ReferalCode> referallist = new List<ReferalCode>();
                    referallist = JsonConvert.DeserializeObject<List<ReferalCode>>
                                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetReferalRecordByreferalCode}?referalCode={referalCode.ReferralCode}"));
                    if(referallist.Count < 5)
                    {
                        ReferalCode getRecord = new ReferalCode();

                        getRecord = JsonConvert.DeserializeObject<ReferalCode>
                                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetReferalRecordByreferalUserID}?referalUserID={referalCode.ReferredUser}"));
                        if(getRecord == null)
                        {
                            ReferalCode referalCodeVM = new ReferalCode()
                            {
                                ReferralCode = referalCode.ReferralCode,
                                ReferredUser = referalCode.ReferredUser,
                                CreatedOn = DateTime.Now,
                                CreatedBy = referalCode.CreatedBy,
                                JobQuotationId = referalCode.JobQuotationId,
                                IsJobComplete = referalCode.IsJobComplete,
                                UserId = sharedRefferalUserID,
                                RefferalAmount = referalCode.RefferalAmount
                            };

                            response = JsonConvert.DeserializeObject<Response>
                                (await httpClient.PostAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.AddEditPromotionReferalCode}", referalCodeVM));
                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "You are already used reffral code.";
                        }
                       
                    }
                    else
                    {
                        response.Message = "Reward Completed";
                    }
                    
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Invalid Refferal";
                }

                

                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<Response> AddPromotionRedemptions(PromotionRedemptions proRedemption)
        {
            Response response = new Response();

            try
            {
                        PromotionRedemptions getRecord = new PromotionRedemptions();

                        getRecord = JsonConvert.DeserializeObject<PromotionRedemptions>
                                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.PackagesAndPayments.GetPromotionRedeemRecordByRedeemUserID}?redeemByUserID={proRedemption.RedeemBy}&promotionTypeID={proRedemption.PromotionId}"));
                        if (getRecord == null)
                        {
                            PromotionRedemptions proRedemptionVM = new PromotionRedemptions()
                            {
                                PromotionId = proRedemption.PromotionId,
                                RedeemBy = proRedemption.RedeemBy,
                                RedeemOn = DateTime.Now,
                                TotalDiscount = proRedemption.TotalDiscount,
                                JobQuotationId = proRedemption.JobQuotationId,
                                CustomerId = proRedemption.CustomerId
                            };

                            response = JsonConvert.DeserializeObject<Response>
                                    (await httpClient.PostAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.PackagesAndPayments.AddPromotionRedemptions}", proRedemptionVM));
                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "You are already used promotional code.";
                        }





                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<Response> AddEditRedemptions(Redemptions redemptions)
        {
            Response response = new Response();

            try
            {
                        Redemptions getRecord = new Redemptions();

                        getRecord = JsonConvert.DeserializeObject<Redemptions>
                                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetRedeemRecordByRedeemUserID}?redeemByUserID={redemptions.RedeemBy}&voucherID={redemptions.VoucherId}"));
                        if (getRecord == null)
                        {
                            Redemptions redemptionVM= new Redemptions()
                            {
                                VoucherId = redemptions.VoucherId,
                                RedeemBy = redemptions.RedeemBy,
                                RedeemOn = DateTime.Now,
                                TotalDiscount = redemptions.TotalDiscount,
                                JobQuotationId = redemptions.JobQuotationId
                            };

                            response = JsonConvert.DeserializeObject<Response>
                                    (await httpClient.PostAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.AddEditRedemptions}", redemptionVM));
                        }
                        else
                        {
                            response.Status = ResponseStatus.Restrected;
                            response.Message = "You are already used promotional code.";
                        }





                return response;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return response;
            }
        }
        public async Task<List<ReferalCode>> GetReferalRecordByreferalCode(string referalCode)
        {
            List<ReferalCode> referallist = new List<ReferalCode>();
            try
            {
                referallist = JsonConvert.DeserializeObject<List<ReferalCode>>
                    (await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetReferalRecordByreferalCode}?referalCode={referalCode}"));
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return null;
            }
            return referallist;
        }

        public async Task<string> GetRedemptionById(string userid)
        {
            try
            {
                return await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetRedemptionById}?userId={userid}", "");
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);

                return ex.Message;
            }
        }

        public Task<string> GetReferalByRoleId(string userId)
        {
            return null;
        }

        //public async Task<string> GetReferalByRoleId(string userId)
        //{
        //    try
        //    {
        //        return await httpClient.GetAsync($"{_apiConfig.PromotionApiUrl}{ApiRoutes.Promotion.GetReferalByRoleId}?userId={userId}", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        Exc.AddErrorLog(ex);

        //        return ex.Message;
        //    }
        //}
    }
}
