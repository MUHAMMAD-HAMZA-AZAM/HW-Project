using HW.Utility;
using System;
using System.Collections.Generic;

namespace HW.UserViewModels
{
    public class JobQuotationDetailVM
    {
        public long JobQuotationId { get; set; }
        public long? CategoryId { get; set; }
        public string Catagory { get; set; }
        public List<IdValueVM> SubCatagory{ get; set; }
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public List<ImageVM> Images { get; set; }
        public byte[] video { get; set; }
        public string VideoFileName { get; set; }
        public bool VideoUpdated { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartingDateTime{ get; set; }
        public string JobStartingDate { get; set; }
        public string JobStartingTime { get; set; }
        public long CityId { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public int QuotesQuantity { get; set; }
        public bool SelectiveTradesman { get; set; }
        public long SubSkillId { get; set; }
        public List<IdValueVM> TradesmanList { get; set; }
        public List<IdValueVM> CitiesList { get; set; }
        public TimeSpan? WorkStartTime { get; set; }
        public int? StatusId { get; set; }
    }
}
