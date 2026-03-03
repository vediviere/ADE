using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Services;
using ADE.Seguridad.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADE.Seguridad.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
            return Conflict(new { mensaje = "El email ya está registrado" });

        return Created("", result);
    }


    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok("✅ Acceso concedido: ADMIN");
    }

    [Authorize(Roles = "DOCENTE")]
    [HttpGet("docente-only")]
    public IActionResult DocenteOnly()
    {
        return Ok("✅ Acceso concedido: DOCENTE");
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("debug/usuarios")]
    public async Task<IActionResult> DebugUsuarios([FromServices] SeguridadDbContext db)
    {
    
        var usuarios = await db.Usuarios
        .Select(u => new {
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