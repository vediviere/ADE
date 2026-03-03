using ADE.Seguridad.Application.DTOs;
using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;

namespace ADE.Seguridad.Application.Services;

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
        var passRaw = dto.Password ?? string.Empty;

        var usuario = await _usuarioRepo.GetByEmailAsync(email);

        Console.WriteLine($"LOGIN email='{email}' encontrado={(usuario != null)}");

        if (usuario == null || !usuario.Activo)
            return null;

        Console.WriteLine($"PASS raw='{passRaw}' len={passRaw.Length}");
        Console.WriteLine($"PASS trim='{passRaw.Trim()}' len={passRaw.Trim().Length}");
        Console.WriteLine($"HASH len={(usuario.PasswordHash?.Length ?? 0)}");
        Console.WriteLine($"HASH='{usuario.PasswordHash}'");

        // ✅ prueba 1: tal cual
        var okRaw = BCrypt.Net.BCrypt.Verify(passRaw, usuario.PasswordHash);
        // ✅ prueba 2: trim
        var okTrim = BCrypt.Net.BCrypt.Verify(passRaw.Trim(), usuario.PasswordHash);

        Console.WriteLine($"Verify raw={okRaw}  trim={okTrim}");

        // usa el que funcione
        if (!okRaw && !okTrim)
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