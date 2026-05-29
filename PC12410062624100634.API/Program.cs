using Microsoft.EntityFrameworkCore;
using PC12410062624100634.CORE.Core.Interfaces;
using PC12410062624100634.CORE.Core.Services;
using PC12410062624100634.CORE.Infrastructure.Data;
using PC12410062624100634.CORE.Infrastructure.Repositories;
using PC12410062624100634.CORE.Infrastructure.Shared; // 1. Agregado para reconocer el JWTService

var builder = WebApplication.CreateBuilder(args);

var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");

// Configuración de la base de datos
builder.Services.AddDbContext<TallerMecanicoContext>(options =>
    options.UseSqlServer(cnx));

// 2. Registro de Repositorios (Acceso a Datos)
builder.Services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();

// 3. Registro de Servicios (Lógica de Negocio)
builder.Services.AddScoped<IOrdenServicioService, OrdenServicioService>();

// 4. Registro del Servicio de Seguridad JWT (NUEVO)
builder.Services.AddScoped<IJWTService, JWTService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();