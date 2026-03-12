using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Services;
using ADE.Seguridad.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// 🐾 CAMINO DE MIGAJAS -- 1° PRIMERA PARADA 🐾 Aquí empieza el flujo desde HTTP. 🐾

// 🐾 Este archivo es la puerta de entrada
// 🐾 Aquí llegan login, register y endpoints protegidos
// 🐾 El controller no resuelve lógica, solo recibe y delega

// 🐾 CONTINUAMOS A LA SEGUNDA PARADA => AuthService 🐾

/* 🐾🐾🐾🐾🐾🐾
============================================================================
MICROSERVICIO: ADE.Seguridad
CAPA: API
ARCHIVO: AuthController.cs

Este controller es la PUERTA DE ENTRADA HTTP del microservicio de Seguridad.

Aquí llegan las peticiones del cliente, por ejemplo:
- Login
- Register
- Endpoints protegidos
- Endpoints de prueba

Este archivo NO contiene la lógica completa de autenticación.
Su responsabilidad es:

1. Recibir la petición HTTP
2. Validar el modelo recibido
3. Llamar a la capa Application (AuthService)
4. Regresar una respuesta HTTP adecuada

Flujo general del login:

Cliente / Swagger / Frontend
        ↓
POST /api/Auth/login
        ↓
AuthController
        ↓
AuthService
        ↓
IUsuarioRepository
        ↓
UsuarioRepository
        ↓
SQL Server (adedb.persona)
        ↓
AuthService
        ↓
JwtService
        ↓
AuthController
        ↓
HTTP 200 + Token

Este archivo pertenece a la capa API.
La lógica de negocio vive en la capa Application.
============================================================================
🐾🐾🐾🐾🐾🐾*/

namespace ADE.Seguridad.API.Controllers;

/*🐾🐾🐾🐾🐾🐾
----------------------------------------------------------------------------
CONTROLADOR PRINCIPAL DE AUTENTICACIÓN

Este controlador expone los endpoints relacionados con autenticación
y autorización del sistema.

Depende de AuthService, que es quien contiene la lógica del caso de uso.
----------------------------------------------------------------------------
🐾🐾🐾🐾🐾🐾*/
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    //🐾🐾 Inyección de dependencias del servicio principal de autenticación 🐾🐾
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    /*🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    ENDPOINT: LOGIN

    Este endpoint permite que un usuario se autentique con:
    - correo electrónico
    - contraseña

    Proceso:
    1. Recibe el LoginDto desde el cliente
    2. Valida el modelo recibido
    3. Llama a AuthService.LoginAsync(dto)
    4. Si las credenciales son válidas:
          - genera token JWT
          - devuelve HTTP 200
       Si no son válidas:
          - devuelve HTTP 401

    Este es el punto de entrada principal al flujo de autenticación.
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾🐾*/
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(new { mensaje = "Credenciales incorrectas" });

        return Ok(result);
    }


    /*🐾🐾🐾🐾🐾🐾
----------------------------------------------------------------------------
ENDPOINT: REGISTER

Este endpoint fue diseñado para registrar usuarios nuevos.

IMPORTANTE:
Actualmente el proyecto está trabajando con una BD existente (Ruta B),
por lo que el registro puede estar deshabilitado temporalmente o requerir
una implementación específica sobre la tabla real de personas.

Si se habilita completamente, el flujo sería:
1. Recibir datos del nuevo usuario
2. Validar duplicados
3. Crear usuario en la fuente de datos real
4. Generar token opcionalmente
----------------------------------------------------------------------------
🐾🐾🐾🐾🐾🐾*/
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        return StatusCode(501, new { mensaje = "Registro deshabilitado temporalmente. Solo Login (Ruta B)." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
            return Conflict(new { mensaje = "El email ya está registrado" });

        return Created("", result);
    }

    // ❔ EndPoint de prueba -- Eliminar o modificar despues
    /* 🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    ENDPOINT: ME

    Este endpoint permite inspeccionar la identidad autenticada actual.

    Sirve para:
    - verificar claims del token
    - probar autorización
    - revisar qué información llegó desde JWT

    Es útil en pruebas con Swagger y durante integración con frontend.
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾🐾*/
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

    // ❔ EndPoint de prueba -- Eliminar o modificar despues
    /*🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    ENDPOINT: ADMIN-ONLY

    Endpoint de prueba protegido por rol.
    Solo puede ser accedido por usuarios cuyo JWT contenga el rol ADMIN.

    Sirve para validar:
    - autenticación
    - autorización por roles
    - correcta lectura del claim Role
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾🐾*/
    [Authorize(Roles = "ADMIN")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("✅ Acceso concedido: ADMIN");
    }

    // ❔ EndPoint de prueba -- Eliminar o modificar despues
    /*🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    ENDPOINT: DOCENTE-ONLY

    Endpoint de prueba protegido por rol.
    Solo puede ser accedido por usuarios cuyo JWT contenga el rol DOCENTE.

    Sirve para validar que el sistema diferencia correctamente roles.
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾*/
    [Authorize(Roles = "DOCENTE")]
    [HttpGet("docente-only")]
    public IActionResult DocenteOnly()
    {
        return Ok("✅ Acceso concedido: DOCENTE");
    }

    // ❔ EndPoint de prueba -- Eliminar despues
    /*🐾🐾🐾🐾🐾🐾
    ----------------------------------------------------------------------------
    ENDPOINT: DEBUG/USUARIOS

    Endpoint de apoyo para desarrollo y depuración.

    Permite consultar usuarios de prueba o información auxiliar del sistema.
    Debe permanecer protegido y no exponerse libremente en producción.

    Actualmente está pensado como herramienta interna de validación.
    ----------------------------------------------------------------------------
    🐾🐾🐾🐾🐾🐾*/
    [Authorize(Roles = "ADMIN")]
    [HttpGet("debug/usuarios")]
    public async Task<IActionResult> DebugUsuarios([FromServices] SeguridadDbContext db)
    {

        var usuarios = await db.Usuarios
        .Select(u => new
        {
            u.Id,
            u.Email,
            HashLen = u.PasswordHash.Length,
            u.Activo,
            u.IdPersona,
            u.IdRol
        })
        .ToListAsync();

        return Ok(usuarios);
    }
}