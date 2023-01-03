using AutoMapper;
using RandomStore.Repository.Context;
using RandomStore.Services.Models.CategoryModels;
using RandomStore.Services.Models.OrderDetailModels;
using RandomStore.Services.Models.OrderModels;
using RandomStore.Services.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProductCreateModel, Product>();
            CreateMap<ProductUpdateModel, Product>();
            CreateMap<Product, ProductGetModel>();
            CreateMap<CategoryCreateModel, Category>();
            CreateMap<CategoryUpdateModel, Category>();
            CreateMap<Category, CategoryGetModel>();
            CreateMap<OrderCreateModel, Order>();
            CreateMap<OrderUpdateModel, Order>();
            CreateMap<Order, OrderGetModel>();
            CreateMap<OrderDetailCreateModel, OrderDetail>();
            CreateMap<OrderDetailUpdateModel, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailGetModel>();
        }
    }
}
