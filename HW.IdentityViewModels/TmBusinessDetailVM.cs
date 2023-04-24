using HW.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW.IdentityViewModels
{
    public class TmBusinessDetailVM
    {
        public TmBusinessDetailVM()
        {
            SkillIds = new List<long>();
        }

        public long TradesmanId { get; set; }

        [Display(Name = "Traveling Distance")]
        public long TravelingDistance { get; set; }

        public bool IsOrganization { get; set; }

        [Display(Name = "Trade Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Registration Number")]
        public string CompanyRegNo { get; set; }

        public long CityId { get; set; }

        public string City { get; set; }

        public string Town { get; set; }
        public long TownId { get; set; }

        [Display (Name ="Business Address")]
        public string BusinessAddress { get; set; }
        public string AddressLine { get; set; }
        public List<long> SkillIds { get; set; }
        public List<IdValueVM> tradesmanSkills { get; set; }
        public string LocationCoordinates { get; set; }
        public string LatLng { get; set; }

    }
}
