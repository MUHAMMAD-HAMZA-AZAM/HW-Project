using System;
using System.Collections.Generic;
using System.Text;

namespace HW.SupplierModels.DTOs.ShippingApiDTO
{
    public class Shipper
    {
        public string name { get; set; }
        public int account_number { get; set; }
        public string phone_number_1 { get; set; }
        public string phone_number_2 { get; set; }
        public string address { get; set; }
        public string origin { get; set; }
    }

    public class Pickup
    {
        public string origin { get; set; }
        public string person_of_contact { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }

    public class Consignee
    {
        public string name { get; set; }
        public string phone_number_1 { get; set; }
        public object phone_number_2 { get; set; }
        public string destination { get; set; }
        public string address { get; set; }
    }

    public class Item
    {
        public string product_type { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
    }

    public class OrderInformation
    {
        public List<Item> items { get; set; }
        public float weight { get; set; }
        public string instructions { get; set; }
        public string shipping_mode { get; set; }
        public int amount { get; set; }
    }

    public class TrackingHistory
    {
        public string date_time { get; set; }
        public string status { get; set; }
        public object status_reason { get; set; }
    }

    public class Details
    {
        public string tracking_number { get; set; }
        public Shipper shipper { get; set; }
        public Pickup pickup { get; set; }
        public Consignee consignee { get; set; }
        public OrderInformation order_information { get; set; }
        public List<TrackingHistory> tracking_history { get; set; }
    }

    public class OrderItemShipmentTrackingDTO : ShippingBaseDTO
    {
        public Details details { get; set; }
    }



}
