using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using GutierrezAPI.Models.Entities;
using GutierrezAPI.Repositories;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
#region  Conexión a la base de datos
string? DB = builder.Configuration.GetConnectionString("DbConnectionString");
builder.Services.AddDbContext<LabsysteGutierrezContext>(x =>
{
    x.UseMySql(DB, ServerVersion.AutoDetect(DB));
    x.ConfigureWarnings(warning =>
    {
        warning.Throw();
    });
});
#endregion
#region Agregar Swagger con JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GutierrezApiITESRC", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresa el token obtenido del login",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion
#region Configurar servicios de autenticación y autorización
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
        ValidateLifetime = true
    };
});
#endregion
builder.Services.AddCors();
#region Logger
//Creacion del logger
Log.Logger = new LoggerConfiguration().WriteTo.File("/Logs/logger.txt",rollingInterval:RollingInterval.Day).CreateLogger();
//limpiar proveedores
builder.Logging.ClearProviders();
#endregion
#region Repositorios
builder.Services.AddTransient<Repository<Usuario>>();
builder.Services.AddTransient<Repository<Documento>>();
builder.Services.AddTransient<Repository<Proveedor>>();
builder.Services.AddTransient<ProveedorRepository>();
#endregion
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
