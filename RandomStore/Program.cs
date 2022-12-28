using Microsoft.EntityFrameworkCore;
using RandomStoreRepo;
using AutoMapper;
using RandomStore.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddDbContext<RandomStoreOneDB>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne"))).
    AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
