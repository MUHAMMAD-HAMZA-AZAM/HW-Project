namespace HW.UserViewModels
{
    public class RateSupplierVM
    {
        public long SupplierId { get; set; }
        public long CustomerId { get; set; }
        public int OverallRating { get; set; }
        public int SupplierCommunicatonRating { get; set; }
        public int SupplierServiceQualityRating { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
    }
}
