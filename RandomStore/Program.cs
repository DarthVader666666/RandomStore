using Microsoft.EntityFrameworkCore;
using RandomStoreRepo;
using AutoMapper;
using RandomStore.Application;
using Swashbuckle.AspNetCore.Swagger;
using RandomStore.Services;
using RandomStore.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddDbContext<RandomStoreOneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
