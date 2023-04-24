using HW.SupplierApi.Services;
using HW.SupplierModels.DTOs.ShippingApiDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.SupplierApi.Controllers
{
    [Produces("application/json")]
    public class ShippingApiController : ControllerBase
    {
        private readonly IShippingApiService _shippingApiService;
        public ShippingApiController(IShippingApiService shippingApiService)
        {
            this._shippingApiService = shippingApiService;
        }
        public string Start()
        {
            return "Shipping api has been started";
        }
        [HttpPost]
        public async Task<string> AddPickupAddres(long supplierId)
        {
            return await _shippingApiService.AddPickupAddres(supplierId);
        }
        [HttpPost]
        public async Task<string> BookShipment([FromBody] BookShipmentDTO bookShipmentDTO)
        {
            return await _shippingApiService.BookShipment(bookShipmentDTO);
        }

        [HttpPost]
        public async Task<string> TrackOrderShipment([FromBody] OrderItemTrackingDTO orderItemTrackingDTO)
        {
            return await _shippingApiService.TrackOrderItemShipment(orderItemTrackingDTO);
        }
        [HttpPost]
        public async Task<string> TrackOrderItemShipment([FromBody] OrderItemTrackingDTO orderItemTrackingDTO)
        {
            return await _shippingApiService.TrackOrderItemShipment(orderItemTrackingDTO);
        }
    }
}
