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
using RandomStore.Services.OrderDetailService;
using RandomStore.Repository.Repositories.OrderDetailsRepositories;
using RandomStore.Services.Models.OrderDetailModels;

var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddScoped<IProductService, ProductService>(provider =>
                new ProductService(new RandomStoreProductRepository(
                    provider.GetService<RandomStoreOneDbContext>()),
                    new Mapper(new MapperConfiguration(config =>
                    {
                        config.CreateMap<ProductCreateModel, Product>();
                        config.CreateMap<ProductUpdateModel, Product>();
                        config.CreateMap<Product, ProductGetModel>();
                    })),
                    logger));

            builder.Services.AddScoped<ICategoryService, CategoryService>(provider =>
                new CategoryService(new RandomStoreCategoryRepository(
                    provider.GetService<RandomStoreOneDbContext>()),
                    new Mapper(new MapperConfiguration(config =>
                    {
                        config.CreateMap<CategoryCreateModel, Category>();
                        config.CreateMap<CategoryUpdateModel, Category>();
                        config.CreateMap<Category, CategoryGetModel> ();
                    })), 
                    logger));

            builder.Services.AddScoped<IOrderService, OrderService>(provider =>
                new OrderService(new RandomStoreOrderRepository(
                    provider.GetService<RandomStoreOneDbContext>()), 
                    new Mapper(new MapperConfiguration(config =>
                    {
                        config.CreateMap<OrderCreateModel, Order>();
                        config.CreateMap<OrderUpdateModel, Order>();
                        config.CreateMap<Order, OrderGetModel>();
                    })), 
                    logger));

            builder.Services.AddScoped<IOrderDetailService, OrderDetailService>(provider =>
                new OrderDetailService(new RandomStoreOrderDetailRepository(
                    provider.GetService<RandomStoreOneDbContext>()),
                    new RandomStoreOrderRepository(provider.GetService<RandomStoreOneDbContext>()),
                    new RandomStoreProductRepository(provider.GetService<RandomStoreOneDbContext>()),
                    new Mapper(new MapperConfiguration(config =>
                    {
                        config.CreateMap<OrderDetailCreateModel, OrderDetail>();
                        config.CreateMap<OrderDetailUpdateModel, OrderDetail>();
                        config.CreateMap<OrderDetail, OrderDetailGetModel>();
                    })), 
                    logger));

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
