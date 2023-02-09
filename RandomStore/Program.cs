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
using RandomStore.Services.Models.OrderDetailsModels;
using RandomStore.Application;

var builder = WebApplication.CreateBuilder(args);

var loggerFactory = LoggerFactory.Create(logBuilder =>
{
    logBuilder.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "errors.txt"));
    logBuilder.AddFilter("System", LogLevel.Error);
});

builder.Services.AddSingleton(typeof(ILogger), loggerFactory.CreateLogger("ServerErrorLogger"));

builder.Services.
    AddDbContext<RandomStoreOneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailsRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();

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
