using _3M.DataModels.Account;
using _3M.DataModels.Products;
using _3M.DataModels.Sales;
using _3M.Dtos.Account;
using _3M.Dtos.Products;
using _3M.Dtos.Sales;
using _3M.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels.MapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Property, propertyDto>();
            CreateMap<Product, ProductDto>();

            CreateMap<UserProfile, UserProfileDto>();

            CreateMap<Bill, BillDto>();
            CreateMap<ShoppingItem, ShoppingItemDto>();
        }
    }
}
