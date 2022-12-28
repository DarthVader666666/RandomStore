using AutoMapper;
using RandomStore.Application.Models.ProductModels;
using RandomStoreRepo.Entities;

namespace RandomStore.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            this.CreateMap<ProductCreateModel, Product>();
        }
    }
}
