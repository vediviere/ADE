using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Services;
using ADE.Seguridad.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADE.Seguridad.API.Controllers;

// 🐾 CAMINO DE MIGAJAS -- 1° PRIMERA PARADA 🐾 Aquí empieza el flujo desde HTTP. 🐾

// 🐾 Este archivo es la puerta de entrada
// 🐾 Aquí llegan login, register y endpoints protegidos
// 🐾 El controller no resuelve lógica, solo recibe y delega

// 🐾 CONTINUAMOS A LA SEGUNDA PARADA => AuthService 🐾

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    // EndPoint de LOGIN -- Cambiar los mensajes
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


    //REFACTORIZAR EndPoint de Registrar -- Cambiar los mensajes y refactorizar 
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
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

    // ❔ EndPoint de prueba -- Eliminar o modificar despues
    [Authorize(Roles = "ADMIN")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("✅ Acceso concedido: ADMIN");
    }

    // ❔ EndPoint de prueba -- Eliminar o modificar despues
    [Authorize(Roles = "DOCENTE")]
    [HttpGet("docente-only")]
    public IActionResult DocenteOnly()
    {
        return Ok("✅ Acceso concedido: DOCENTE");
    }

    // ❔ EndPoint de prueba -- Eliminar despues
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