using Microsoft.EntityFrameworkCore;
using RandomStoreRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RandomStoreOneDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RandomStoreOne")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
