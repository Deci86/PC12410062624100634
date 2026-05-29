using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.Interfaces;
using PC12410062624100634.CORE.Core.Services;
using PC12410062624100634.CORE.Infrastructure.Data;
using PC12410062624100634.CORE.Infrastructure.Repositories;
using PC12410062624100634.CORE.Infrastructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");

// SQL Server (Activo)
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(cnx));

// PostreSQL (Comentado/Desactivado)
// builder.Services.AddDbContext<StoreDbContext>(options =>
//    options.UseNpgsql(cnx));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSharedInfrastructure(_config);
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();