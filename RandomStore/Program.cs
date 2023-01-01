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
using RandomStore.Application.Loggers;
using RandomStore.Services.OrderService;
using RandomStore.Repository.Repositories.OrderRepositories;
using RandomStore.Services.Models.OrderModels;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var loggerFactory = LoggerFactory.Create(logBuilder =>
{
    logBuilder.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "errors.txt"));
    logBuilder.AddFilter("System", LogLevel.Error);
});

var logger = loggerFactory.CreateLogger("ServerErrorLogger");

switch (builder.Configuration["Repository"].ToUpper())
{
    case "RANDOM_STORE_ONE":
    {
            builder.Services.
                AddDbContext<RandomStoreOneDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

            //builder.Services.AddScoped<IProductRepository, RandomStoreProductRepository>();
            //builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IProductService, ProductService>(provider =>
                new ProductService(new RandomStoreProductRepository(
                    provider.GetService<RandomStoreOneDbContext>()),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductCreateModel, Product>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<ProductUpdateModel, Product>())), logger));

            builder.Services.AddScoped<ICategoryService, CategoryService>(provider =>
                new CategoryService(new RandomStoreCategoryRepository(
                    provider.GetService<RandomStoreOneDbContext>()), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<CategoryCreateModel, Category>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<CategoryUpdateModel, Category>())), logger));

            builder.Services.AddScoped<IOrderService, OrderService>(provider =>
                new OrderService(new RandomStoreOrderRepository(
                    provider.GetService<RandomStoreOneDbContext>()), 
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<OrderCreateModel, Order>())),
                    new Mapper(new MapperConfiguration(config =>
                    config.CreateMap<OrderUpdateModel, Order>())), logger));

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
