using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RandomStore.Services;
using RandomStore.Services.ProductService;
using RandomStore.Repository.Repositories;
using RandomStoreRepo.Entities;
using RandomStore.Services.Models.ProductModels;
using RandomStore.Repository.Context;

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
                    provider.GetService<RandomStoreOneDbContext>()!), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductCreateModel, Product>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductUpdateModel, Product>())
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
    if (app.Configuration["StartSwagger"] == "true")
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        endpoints.MapSwagger();
    }
    else
    { 
        endpoints.MapControllers();
    }
});

app.Run();
