using AutoMapper;
using HW.SupplierModels;
using HW.SupplierModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.SupplierApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductSubCategory, ProductSubCategoryDTO>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Supplier, SupplierShopDTO>();
            CreateMap<State, StateDTO>();
            CreateMap<Area, AreaDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<Banks, BankDTO>();
            CreateMap<WherehouseAddress, WhareHouseAddressDTO>();
        }
    }
}
