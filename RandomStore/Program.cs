using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RandomStore.Services;
using RandomStore.Services.ProductService;
using RandomStoreRepo.Entities;
using RandomStore.Services.Models.ProductModels;
using RandomStore.Repository.Context;
using RandomStore.Repository.Repositories.ProductRepositories;
using RandomStore.Services.CategoryService;
using RandomStore.Services.Models.CategoryModels;
using RandomStore.Repository.Repositories.CategoryRepositories;

var builder = WebApplication.CreateBuilder(args);

switch (builder.Configuration["Repository"].ToUpper())
{
    case "RANDOM_STORE_ONE":
    {
            builder.Services.
                AddDbContext<RandomStoreOneDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

            builder.Services.AddScoped<IProductService, ProductService>(provider =>
                new ProductService(new RandomStoreProductRepository(
                    provider.GetService<RandomStoreOneDbContext>()), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductCreateModel, Product>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductUpdateModel, Product>())
                    )));

            builder.Services.AddScoped<ICategoryService, CategoryService>(provider =>
                new CategoryService(new RandomStoreCategoryRepository(
                    provider.GetService<RandomStoreOneDbContext>()), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<CategoryCreateModel, Category>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<CategoryUpdateModel, Category>())
                    )));
            
            break;
    }
}

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    if (bool.Parse(app.Configuration["StartSwagger"]))
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
            { 
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            }
        );
    }
    
    endpoints.MapControllers();
});

app.Run();
