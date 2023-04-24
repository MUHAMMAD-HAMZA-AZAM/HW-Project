using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
   public class BookShipmentDTO
    {
        public int service_type_id { get; set; }
        public long pickup_address_id { get; set; }
        public int information_display { get; set; }
        public long consignee_city_id { get; set; }
        public string consignee_name { get; set; }
        public string consignee_address { get; set; }
        public string consignee_phone_number_1 { get; set; }
        public string consignee_email_address { get; set; }
        public string order_id { get; set; }
        public long item_product_type_id { get; set; }
        public string item_description { get; set; }
        public long item_quantity { get; set; }
        public long item_insurance { get; set; }
        public decimal item_price { get; set; }
        public DateTime pickup_date { get; set; }
        public string special_instructions { get; set; }
        public decimal estimated_weight { get; set; }
        public int shipping_mode_id { get; set; }
        public decimal amount { get; set; }
        public int payment_mode_id { get; set; }
        public int charges_mode_id { get; set; }
        public long open_box { get; set; }
    }
}
