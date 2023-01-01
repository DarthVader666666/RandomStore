using AutoMapper;
using RandomStore.Services.Models.CategoryModels;
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
            CreateMap<CategoryCreateModel, Category>();
            CreateMap<CategoryUpdateModel, Category>();
        }
    }
}
