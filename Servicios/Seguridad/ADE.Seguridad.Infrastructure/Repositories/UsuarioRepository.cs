using ADE.Seguridad.Application.Interfaces;
using ADE.Seguridad.Domain.Entities;
using ADE.Seguridad.Infrastructure.Data.Scaffold;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace ADE.Seguridad.Infrastructure.Repositories;

// 🐾 CAMINO DE MIGAJAS -- 4° CUARTA PARADA 🐾 Aquí está la conexión real con datos. 🐾

//Aquí se consulta "adedb.persona"
//Se transforma una entidad de BD a un Usuario del dominio
//Se traduce "id_rol" a nombre de rol

// 🐾 CONTINUAMOS A LA QUINTA PARADA => AdeDbContext 🐾

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AdeDbContext _context;

    public UsuarioRepository(AdeDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        var p = await _context.personas
            .FirstOrDefaultAsync(x => x.correo_inst.Trim().ToLower() == normalized);

        if (p == null) return null;

        var rolNombre = p.id_rol switch
        {
            1 => "CARRERA",
            2 => "DOCENTE",
            3 => "ESTUDIANTE",
            4 => "JEFATURA",
            5 => "ADMIN",
            6 => "SUPERADMIN",
            _ => "USER"
        };

        return new Usuario
        {
            Id = p.id_persona,
            Email = p.correo_inst,
            PasswordHash = p.contrasena,
            Activo = (p.status ?? "").ToUpper() == "ACTIVO" || (p.status ?? "") == "1",
            IdPersona = p.id_persona,
            IdRol = p.id_rol,
            Rol = new Rol { Id = p.id_rol, Nombre = rolNombre }
        };
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        var p = await _context.personas
            .FirstOrDefaultAsync(x => x.id_persona == id);

        if (p == null)
            return null;

        var rolNombre = p.id_rol switch
        {
            1 => "CARRERA",
            2 => "DOCENTE",
            3 => "ESTUDIANTE",
            4 => "JEFATURA",
            5 => "ADMIN",
            6 => "SUPERADMIN",
            _ => "USER"
        };

        return new Usuario
        {
            Id = p.id_persona,
            Email = p.correo_inst,
            PasswordHash = p.contrasena,
            Activo = (p.status ?? "").ToUpper() == "ACTIVO" || (p.status ?? "") == "1",
            IdPersona = p.id_persona,
            IdRol = p.id_rol,
            Rol = new Rol
            {
                Id = p.id_rol,
                Nombre = rolNombre
            }
        };
    }

    public async Task<bool> ExisteEmailAsync(string email)
    {
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        return await _context.personas
            .AnyAsync(p => p.correo_inst.Trim().ToLower() == normalized);
    }

    public async Task ActualizarPasswordHashAsync(int idPersona, string newHash)
    {
        var persona = await _context.personas.FirstOrDefaultAsync(p => p.id_persona == idPersona);
        if (persona == null) return;

        persona.contrasena = newHash;
        await _context.SaveChangesAsync();
    }

    public Task<Usuario> CrearAsync(Usuario usuario)
    {
        throw new NotImplementedException(
            "CrearAsync está deshabilitado en Ruta B (DB existente). " +
            "Por ahora solo se permite Login contra adedb.persona. " +
            "Si se decide permitir registro, se implementará insertando en adedb.persona."
        );
    }
}