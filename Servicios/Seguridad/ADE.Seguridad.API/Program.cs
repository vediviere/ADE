using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Application.Services;
using ADE.Seguridad.Infrastructure.Data.Scaffold;
using ADE.Seguridad.Infrastructure.Repositories;
using ADE.Seguridad.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

// 🐾 CAMINO DE MIGAJAS -- 7° SEPTIMA PARADA 🐾 Aquí se configura todo el sistema 🐾

//Se registra el DbContext
//Se registran repositorios y servicios
//Se configura JWT
//Aquí se define CORS, Swagger y autorización 🐾


// 🐾🐾 Creamos el builder para configurar la aplicación 🐾🐾
var builder = WebApplication.CreateBuilder(args);


// 🐾🐾 Aquí se conecta con la base de datos real, usando la cadena de conexión definida en appsettings.json 🐾🐾
builder.Services.AddDbContext<AdeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ADESeguridadDb"));
});

// 🐾🐾 Registramos el repositorio de usuarios y el servicio de JWT para que puedan ser inyectados en los controladores 🐾🐾
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// 🐾🐾 Registramos el servicio de autenticación que usará el repositorio y el servicio de JWT 🐾🐾
builder.Services.AddScoped<IJwtService, JwtService>();

// 🐾🐾 Este servicio es el que realmente hace la lógica de autenticación, usando el repositorio para validar credenciales y el servicio de JWT para generar tokens 🐾🐾
builder.Services.AddScoped<AuthService>();

// 🐾🐾 Configuramos la autenticación JWT, definiendo cómo se validarán los tokens que lleguen en las solicitudes 🐾🐾
var jwtKey = builder.Configuration["Jwt:Key"]!;

// 🐾🐾 Aquí le decimos a ASP.NET que use JWT Bearer tokens para autenticarse, y definimos los parámetros de validación del token 🐾🐾
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // 🐾🐾 Validamos la firma del token con la clave secreta definida en la configuración 🐾🐾
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            // 🐾🐾 Validar expiración 🐾🐾
            ValidateLifetime = true,

            // 🐾🐾 Decirle a ASP.NET cuál claim es "rol" 🐾🐾
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,

            // 🐾🐾 Cuál claim usar como Name -- LUEGO LO ARREGLO BIEN 🐾🐾
            NameClaimType = System.Security.Claims.ClaimTypes.Email,

            ClockSkew = TimeSpan.Zero
        };
    });

// 🐾🐾 Agregamos autorización para que podamos usar [Authorize] en los controladores y proteger rutas 🐾🐾
builder.Services.AddAuthorization();

// 🐾🐾 Configuramos CORS para permitir que el ANGULAR pueda hacer solicitudes a esta API sin problemas de origen cruzado 🐾🐾
builder.Services.AddCors(options =>
{
    // 🐾🐾 Aquí definimos una política de CORS llamada "AdePolicy" que permite solicitudes desde "http://localhost:4200" (donde corre el Angular) y permite cualquier encabezado y método HTTP 🐾🐾

    // Esto debe de cambiarse por el puerto que diga LES!!!
    options.AddPolicy("AdePolicy", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// 🐾🐾 Configuracion de Swagger 🐾🐾 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 🐾🐾 Aquí definimos la información básica de la API que aparecerá en Swagger 🐾🐾
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ADE - Seguridad API", Version = "v1" });
    // 🐾🐾 Aquí definimos el esquema de seguridad para JWT Bearer tokens, indicando que se debe enviar un encabezado "Authorization" con el formato "Bearer {token}" 🐾🐾
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe: Bearer {tu token JWT}"
    });

    // Todas las rutas protegidas requieren el esquema de seguridad "Bearer" que definimos arriba
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                // 🐾🐾 Indicamos que el tipo de referencia es un esquema de seguridad, y el ID es "Bearer" para que Swagger sepa que todas las rutas protegidas deben usar este esquema 🐾🐾
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
});
});

// 🐾🐾 Construimos la aplicación con la configuración que hemos definido hasta ahora 🐾🐾
var app = builder.Build();

/* 🐾🐾 En esta sección se configura el pipeline HTTP de ASP.NET Core.

        El pipeline es la cadena de middleware que procesa cada solicitud
        desde que llega al servidor hasta que se devuelve la respuesta.

        Cada middleware puede:
        -leer la solicitud
        - modificarla
        - validarla
        - detener el flujo
        - pasar la solicitud al siguiente componente
 🐾🐾 */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🐾🐾 Le decimos a ASP.NET que use HTTPS redirection para redirigir automáticamente las solicitudes HTTP a HTTPS, lo cual es importante para la seguridad de la API 🐾🐾
app.UseCors("AdePolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();