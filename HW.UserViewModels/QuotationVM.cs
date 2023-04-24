using HW.TradesmanViewModels;
using HW.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HW.UserViewModels
{
    public class QuotationVM
    {
        public long UserId { get; set; }
        public long JobQuotationId { get; set; }
        public string WorkTitle { get; set; }
        public string JobDescription { get; set; }
        public decimal Budget { get; set; }
        public string AddressLine { get; set; }
        public long? TownId { get; set; }
        public string Town { get; set; }
        public string Area { get; set; }
        public string StreetAddress { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long CityId { get; set; }
        public DateTime JobstartDateTime { get; set; }
        public string JobStartTime { get; set; }
        public int NumberOfBids { get; set; }
        public string LocationCoordinates { get; set; }
        public string CreatedBy { get; set; }
        public List<ImageVM> ImageVMs { get; set; }
        public List<long> FavoriteIds { get; set; }
        public VideoVM VideoVM { get; set; }
        public string SkillName { get; set; }
        public string CityName { get; set; }

        public bool SelectiveTradesman { get; set; }
        public int StatusId { get; set; }
        public decimal? VisitCharges { get; set; }
        public decimal? ServiceCharges { get; set; }
        public TimeSpan? WorkStartTime { get; set; }
        public string GpsCoordinates { get; set; }
        public string SubSkillName { get; set; }
        public List<PersonalDetailVM> fireBaseIds{ get; set; }
    }
    
}
