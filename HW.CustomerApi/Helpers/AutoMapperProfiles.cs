using AutoMapper;
using HW.CustomerModels;
using HW.CustomerModels.DTOs;
using HW.SupplierModels;
using HW.SupplierModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.CustomerApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductsWishList, ProductsWishListDTO>();
          
        }
    }
}
