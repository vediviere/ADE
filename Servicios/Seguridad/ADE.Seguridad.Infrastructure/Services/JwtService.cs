using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace ADE.Seguridad.Infrastructure.Services;

// 🐾 CAMINO DE MIGAJAS -- 6° SEXTA PARADA 🐾 Aquí se genera el token 🐾

//Se transforman datos del usuario en claims
//Se agregan email, rol e idPersona al token
//aquí se firma el JWT

// 🐾 CONTINUAMOS A LA SEPTIMA PARADA => Program 🐾
public class JwtService : IJwtService
{
    // 🐾🐾 Traemos la configuración para acceder a las claves y parámetros del JWT 🐾🐾
    private readonly IConfiguration _config;

    // 🐾🐾 Inyectamos la configuración a través del constructor 🐾🐾
    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    // 🐾🐾 Genera un token JWT para un usuario dado 🐾🐾
    public string GenerarToken(Usuario usuario)
    {
        // 🐾🐾 Creamos la clave de seguridad a partir de la clave secreta en la configuración - (appsettings.json) 🐾🐾
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        // 🐾🐾 Creamos las credenciales de firma utilizando la clave HMAC SHA256 🐾🐾
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 🐾🐾 Definimos los claims que se incluirán en el token, como el ID del usuario, email, rol y idPersona 🐾🐾
        /* 🐾🐾 Claims:
            Son atributos que describen al usuario autenticado.
            Se incluyen dentro del JWT y permiten que el servidor dentifique al usuario sin consultar la base de datos
            En este caso, se incluyen: 
                            - NameIdentifier: el ID del usuario
                            - Email: el correo electrónico del usuario
                            - Role: el rol del usuario (o "USER" si no tiene rol asignado)
                            - id_persona: el ID de la persona asociada al usuario
        🐾🐾 */

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "USER"),
            new Claim("id_persona", usuario.IdPersona.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // 🐾🐾 Creamos el token JWT con el emisor, audiencia, claims, fecha de expiración y credenciales de firma 🐾🐾
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // 🐾🐾 Valida un token JWT y devuelve true si es válido, false si no lo es 🐾🐾
    public bool ValidarToken(string token)
    {
        try
        {
            // 🐾🐾 Creamos un manejador de tokens JWT para validar el token recibido 🐾🐾
            var tokenHandler = new JwtSecurityTokenHandler();
            // 🐾🐾 Obtenemos la clave de seguridad a partir de la configuración para validar la firma del token 🐾🐾
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            // 🐾🐾 Validamos el token utilizando los parámetros de validación definidos, como la clave de firma, emisor, audiencia y sin tolerancia de reloj - sin tolerancia de reloj POR AHORA 🐾🐾
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}