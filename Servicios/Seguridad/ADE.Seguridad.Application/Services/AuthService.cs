using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;
using System.Diagnostics;

// 🐾 CAMINO DE MIGAJAS -- 2° SEGUNDA PARADA 🐾 Aquí vive la lógica del caso de uso. 🐾

// 🐾 Aquí se procesa el login
// 🐾 Se valida usuario activo
// 🐾 Se valida contraseña
// 🐾 Se pide generar el JWT
// 🐾 Se arma la respuesta final


// 🐾 CONTINUAMOS A LA TERCERA PARADA => IUsuarioRepository 🐾

/*🐾🐾🐾🐾🐾🐾
============================================================================
MICROSERVICIO: ADE.Seguridad
CAPA: Application
ARCHIVO: AuthService.cs

Este servicio contiene la LÓGICA DE NEGOCIO del proceso de autenticación.

Aquí no se reciben peticiones HTTP directamente y aquí tampoco se accede
a la base de datos de forma directa.

Su función es coordinar el caso de uso:

- buscar usuario
- validar si existe
- validar si está activo
- validar contraseña
- generar JWT
- devolver una respuesta estructurada al controller

Este archivo se encuentra en la capa Application, por lo tanto:
- conoce interfaces
- conoce DTOs
- conoce entidades de dominio
- NO conoce detalles internos de Entity Framework
- NO depende directamente de SQL Server

Flujo del login:

AuthController
    ↓
AuthService.LoginAsync(dto)
    ↓
IUsuarioRepository.GetByEmailAsync(email)
    ↓
UsuarioRepository (Infrastructure)
    ↓
SQL Server / adedb.persona
    ↓
AuthService valida contraseña
    ↓
IJwtService.GenerarToken(usuario)
    ↓
JwtService (Infrastructure)
    ↓
TokenResponseDto
    ↓
AuthController
============================================================================
🐾🐾🐾🐾🐾🐾*/

namespace ADE.Seguridad.Application.Services;

/*🐾🐾🐾🐾🐾🐾
----------------------------------------------------------------------------
SERVICIO PRINCIPAL DE AUTENTICACIÓN

Este servicio representa el caso de uso principal del microservicio
de Seguridad.

Recibe datos desde el controller y coordina:
- consulta de usuario
- validación
- generación de token
- respuesta final
----------------------------------------------------------------------------
🐾🐾🐾🐾🐾🐾*/
public class AuthService
{
    // 🐾🐾 Repositorio para consultar usuarios en la fuente de datos real 🐾🐾
    private readonly IUsuarioRepository _usuarioRepo;
    // 🐾🐾 Servicio para generar y validar tokens JWT 🐾🐾
    private readonly IJwtService _jwtService;

    // 🐾🐾 Inyección de dependencias necesarias para el flujo de autenticación 🐾🐾
    public AuthService(IUsuarioRepository usuarioRepo, IJwtService jwtService)
    {
        _usuarioRepo = usuarioRepo;
        _jwtService = jwtService;
    }

    /*🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    CASO DE USO: LOGIN

    Este método ejecuta el flujo principal de autenticación.

    Responsabilidades:
    1. Normalizar el email recibido
    2. Buscar al usuario por correo
    3. Validar que el usuario exista
    4. Validar que esté activo
    5. Validar la contraseña
    6. Generar token JWT
    7. Construir la respuesta final para el controller

    Entrada:
    - LoginDto con email y password

    Salida:
    - TokenResponseDto si el login es correcto
    - null si las credenciales son inválidas o el usuario no puede acceder

    Observación:
    Actualmente la autenticación está conectada a la BD real ADE
    utilizando la tabla adedb.persona como fuente de identidad.
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾🐾*/
    public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
    {
        // 🐾🐾 Normalización de credenciales para evitar errores por espacios o mayúsculas 🐾🐾
        var email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
        var pass = (dto.Password ?? string.Empty).Trim();

        // 🐾🐾 Consulta del usuario en la fuente de datos real a través del repositorio 🐾🐾
        var usuario = await _usuarioRepo.GetByEmailAsync(email);

        // 🐾🐾 Si el usuario no existe o no está activo, se rechaza el acceso 🐾🐾
        if (usuario == null || !usuario.Activo)
            return null;

        // 🐾🐾 Por ahora: BD guarda texto plano, tenemos que migrar a hash 🐾🐾
        if ((usuario.PasswordHash ?? string.Empty) != pass)
            return null;

        // 🐾🐾 Genera del JWT con la información del usuario autenticado 🐾🐾
        var token = _jwtService.GenerarToken(usuario);

        // 🐾🐾 Construcción de la respuesta que será enviada al controller 🐾🐾
        return new TokenResponseDto
        {
            Token = token,
            Email = usuario.Email,
            Rol = usuario.Rol?.Nombre ?? "USER",
            IdPersona = usuario.IdPersona,
            Expiracion = DateTime.UtcNow.AddHours(8)
        };
    }

    // 🐾🐾 ❌ Aqui tengo que ver las contraseñas para HASH 🐾🐾

}