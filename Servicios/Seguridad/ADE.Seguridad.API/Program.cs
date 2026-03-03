using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Application.Services;
using ADE.Seguridad.Domain.Entities;
using ADE.Seguridad.Infrastructure.Data;
using ADE.Seguridad.Infrastructure.Repositories;
using ADE.Seguridad.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ Root compartido (misma BD InMemory para todos los DbContext)
builder.Services.AddSingleton<InMemoryDatabaseRoot>();

builder.Services.AddDbContext<SeguridadDbContext>((sp, options) =>
{
    var root = sp.GetRequiredService<InMemoryDatabaseRoot>();
    options.UseInMemoryDatabase("SeguridadDb", root);
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<AuthService>();

var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // ✅ IMPORTANTE: validar expiración
            ValidateLifetime = true,

            // ✅ IMPORTANTE: decirle a ASP.NET cuál claim es "rol"
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,

            // (Opcional recomendado) cuál claim usar como Name
            NameClaimType = System.Security.Claims.ClaimTypes.Email,

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AdePolicy", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ADE - Seguridad API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe: Bearer {tu token JWT}"
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AdePolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ✅ MUY IMPORTANTE para InMemory: crea el modelo y aplica HasData
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SeguridadDbContext>();
    db.Database.EnsureCreated();

    // ✅ por si HasData no se aplica como esperas, aseguramos roles manualmente
    if (!db.Roles.Any())
    {
        db.Roles.AddRange(
            new Rol { Id = 1, Nombre = "ADMIN" },
            new Rol { Id = 2, Nombre = "DOCENTE" },
            new Rol { Id = 3, Nombre = "ESTUDIANTE" },
            new Rol { Id = 4, Nombre = "JEFATURA" }
        );
        db.SaveChanges();
    }
}

//prueba

app.Run();