using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;
using System.Diagnostics;

namespace ADE.Seguridad.Application.Services;


// 🐾 CAMINO DE MIGAJAS -- 2° SEGUNDA PARADA 🐾 Aquí vive la lógica del caso de uso. 🐾

// 🐾 Aquí se procesa el login
// 🐾 Se valida usuario activo
// 🐾 Se valida contraseña
// 🐾 Se pide generar el JWT
// 🐾 Se arma la respuesta final


// 🐾 CONTINUAMOS A LA TERCERA PARADA => IUsuarioRepository 🐾

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepo;
    private readonly IJwtService _jwtService;

    public AuthService(IUsuarioRepository usuarioRepo, IJwtService jwtService)
    {
        _usuarioRepo = usuarioRepo;
        _jwtService = jwtService;
    }

    public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
    {
        var email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
        var pass = (dto.Password ?? string.Empty).Trim();

        var usuario = await _usuarioRepo.GetByEmailAsync(email);

        if (usuario == null || !usuario.Activo)
            return null;

        // ✅ Por ahora: BD guarda texto plano, comparamos directo
        if ((usuario.PasswordHash ?? string.Empty) != pass)
            return null;

        var token = _jwtService.GenerarToken(usuario);

        return new TokenResponseDto
        {
            Token = token,
            Email = usuario.Email,
            Rol = usuario.Rol?.Nombre ?? "USER",
            IdPersona = usuario.IdPersona,
            Expiracion = DateTime.UtcNow.AddHours(8)
        };
    }

    public async Task<TokenResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var email = (dto.Email ?? string.Empty).Trim().ToLowerInvariant();
        var passRaw = dto.Password ?? string.Empty;

        Console.WriteLine($"REGISTER email='{email}' passRawLen={passRaw.Length} passTrimLen={passRaw.Trim().Length}");

        if (await _usuarioRepo.ExisteEmailAsync(email))
            return null;

        var usuario = new Usuario
        {
            Email = email,
            // ✅ IMPORTANTÍSIMO: guarda el hash con password TRIM
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(passRaw.Trim()),
            IdPersona = dto.IdPersona,
            IdRol = dto.IdRol,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        var creado = await _usuarioRepo.CrearAsync(usuario);

        // ✅ Re-cargar desde repo para traer Rol con Include(u => u.Rol)
        var creadoConRol = await _usuarioRepo.GetByIdAsync(creado.Id) ?? creado;

        var token = _jwtService.GenerarToken(creadoConRol);

        return new TokenResponseDto
        {
            Token = token,
            Email = creadoConRol.Email,
            Rol = creadoConRol.Rol?.Nombre ?? "USER",
            IdPersona = creadoConRol.IdPersona,
            Expiracion = DateTime.UtcNow.AddHours(8)
        };
    }
}