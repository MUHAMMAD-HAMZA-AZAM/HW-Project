using AutoMapper;
using HW.SupplierApi.Helpers;
using HW.SupplierModels.DTOs;
using HW.SupplierModels.DTOs.ShippingApiDTO;
using HW.Utility;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HW.SupplierApi.Services
{
    public interface IShippingApiService
    {
        Task<string> AddPickupAddres(long supplierId);
        Task<string> BookShipment(BookShipmentDTO bookShipmentDTO);
        Task<string> TrackOrderShipment(OrderTrackingDTO orderTrackingDTO);
        Task<string> TrackOrderItemShipment(OrderItemTrackingDTO orderItemTrackingDTO);
    }
    public class ShippingApiService : IShippingApiService
    {
        private readonly IUnitOfWork uow;
        private readonly IExceptionService Exc;
        private readonly IMapper _mapper;
        private string ApiKey;
        public ShippingApiService(IUnitOfWork uow, IExceptionService Exc, IMapper mapper, IOptions<ShippingApiSettings> config)
        {
            this.uow = uow;
            this.Exc = Exc;
            _mapper = mapper;
            ApiKey = config.Value.ApiKey;
        }
        public static HttpClient GetHttpClientHeader(string apiKey)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiKey);
            return client;
        }

        public async Task<string> AddPickupAddres(long supplierId)
        {
            try
            {
                ResponseDTO responseDTO = new ResponseDTO();
                SqlParameter[] sqlParameter =
                {
               new SqlParameter("@supplierId",supplierId)
            };
                var res = await uow.ExecuteReaderSingleDSNew<AddPickupAddressDTO>("Sp_GetSupplierPickupAddress", sqlParameter);
                var pickupAddress = res.FirstOrDefault();

                if (pickupAddress != null)
                {
                    var url = "https://sonic.pk/api/pickup_address/add";
                    var jsonData = JsonConvert.SerializeObject(pickupAddress);
                    var client = GetHttpClientHeader(ApiKey);
                    HttpContent httpContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var resp = await client.PostAsync(url, httpContent);
                    var responseStr = resp.Content.ReadAsStringAsync().Result;
                    responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(responseStr);
                    if (responseDTO.id > 0)
                    {
                        SqlParameter[] sqlParameter1 =
                        {
                      new SqlParameter("@supplierId",supplierId),
                      new SqlParameter("@pickupAddressId",responseDTO.id)
                    };
                        uow.ExecuteNonQuery<AddPickupAddressDTO>("Sp_UpdatePickupAddressId", sqlParameter1);
                    }
                }

                return JsonConvert.SerializeObject(responseDTO);
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> BookShipment(BookShipmentDTO bookShipmentDTO)
        {
            try
            {
                var url = "https://sonic.pk/api/shipment/book";
                var jsonData = JsonConvert.SerializeObject(bookShipmentDTO);
                var client = GetHttpClientHeader(ApiKey);
                HttpContent httpContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(url, httpContent);
                var responseStr = resp.Content.ReadAsStringAsync().Result;
                return responseStr;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> TrackOrderShipment(OrderTrackingDTO orderTrackingDTO)
        {
            try
            {
                var orderId = TraxPrefixes.OrderPrefix + orderTrackingDTO.order_id.ToString();
                var client = GetHttpClientHeader(ApiKey);
                var resp = await client.GetAsync($"{ApiRoutes.Shipment.OrderTrackShipment}?order_id={orderId}&type={orderTrackingDTO.type}");
                var responseStr = resp.Content.ReadAsStringAsync().Result;
                return responseStr;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }

        public async Task<string> TrackOrderItemShipment(OrderItemTrackingDTO orderItemTrackingDTO)
        {
            try
            {
                var client = GetHttpClientHeader(ApiKey);
                var resp = await client.GetAsync($"{ApiRoutes.Shipment.TrackShipment}?tracking_number={orderItemTrackingDTO.tracking_number}&type={orderItemTrackingDTO.type}");
                var responseStr = resp.Content.ReadAsStringAsync().Result;
                return responseStr;
            }
            catch (Exception ex)
            {
                Exc.AddErrorLog(ex);
                return ex.Message;
            }
        }
    }
}
