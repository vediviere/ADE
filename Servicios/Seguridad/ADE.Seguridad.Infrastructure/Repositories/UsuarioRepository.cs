using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;
using ADE.Seguridad.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ADE.Seguridad.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SeguridadDbContext _context;

    public UsuarioRepository(SeguridadDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == normalized);
    }

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<bool> ExisteEmailAsync(string email)
    {
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        return await _context.Usuarios
            .AnyAsync(u => u.Email.Trim().ToLower() == normalized);
    }

    public async Task<Usuario> CrearAsync(Usuario usuario)
    {
        // ✅ Normaliza SIEMPRE antes de guardar (por si entra con espacios)
        usuario.Email = (usuario.Email ?? string.Empty).Trim().ToLowerInvariant();

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}