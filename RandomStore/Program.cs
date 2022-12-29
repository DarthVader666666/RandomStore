using Microsoft.EntityFrameworkCore;
using RandomStoreRepo;
using AutoMapper;
using RandomStore.Application;
using Swashbuckle.AspNetCore.Swagger;
using RandomStore.Services;
using RandomStore.Services.ProductService;
using RandomStore.Repository.Repositories;
using RandomStoreRepo.Entities;
using RandomStore.Services.Models.ProductModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

switch (builder.Configuration["Repository"].ToUpper())
{
    case "RANDOM_STORE_ONE":
    {
            builder.Services.
                AddDbContext<RandomStoreOneDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

            builder.Services.AddScoped<IProductService, ProductService>(provider =>
                new ProductService(new ProductRepository(
                    provider.GetService<RandomStoreOneDbContext>()!), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductCreateModel, Product>()))));
            
            break;
    }
}

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
