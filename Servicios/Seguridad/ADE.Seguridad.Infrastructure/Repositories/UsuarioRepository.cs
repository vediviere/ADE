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
    // 🐾🐾 Traemos el contexto de la base de datos para hacer consultas 🐾🐾
    private readonly AdeDbContext _context;

    // 🐾🐾 Inyectamos el contexto a través del constructor 🐾🐾
    public UsuarioRepository(AdeDbContext context)
    {
        _context = context;
    }

    // 🐾🐾 Consulta la tabla "persona" para encontrar un usuario por su correo institucional 🐾🐾
    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        // 🐾🐾 Normalizamos el email para evitar problemas de mayúsculas, espacios, etc. 🐾🐾
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        // 🐾🐾 Buscamos la persona en la base de datos por su correo institucional 🐾🐾
        var p = await _context.personas
            .FirstOrDefaultAsync(x => x.correo_inst.Trim().ToLower() == normalized);

        // 🐾🐾 Si no encontramos a nadie, devolvemos null 🐾🐾
        if (p == null) return null;

        // 🐾🐾 Traducimos el id_rol a un nombre de rol legible 🐾🐾
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

        // 🐾🐾 Creamos un objeto Usuario del dominio con los datos de la persona encontrada 🐾🐾
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

    // 🐾🐾 Consulta la tabla "persona" para encontrar un usuario por su ID 🐾🐾
    public async Task<Usuario?> GetByIdAsync(int id)
    {
        // 🐾🐾 Buscamos la persona en la base de datos por su ID 🐾🐾
        var p = await _context.personas
            .FirstOrDefaultAsync(x => x.id_persona == id);

        if (p == null)
            return null;

        // 🐾🐾 Traducimos el id_rol a un nombre de rol legible 🐾🐾
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

        // 🐾🐾 Creamos un objeto Usuario con los datos de la persona encontrada 🐾🐾
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

    // 🐾🐾 Verifica si ya existe un usuario con el mismo correo institucional 🐾🐾
    public async Task<bool> ExisteEmailAsync(string email)
    {
        // 🐾🐾 Normalizamos el email para la comparación 🐾🐾
        var normalized = (email ?? string.Empty).Trim().ToLowerInvariant();

        return await _context.personas
            .AnyAsync(p => p.correo_inst.Trim().ToLower() == normalized);
    }

    // 🐾🐾 Actualiza el hash de la contraseña para un usuario específico 🐾🐾
    public async Task ActualizarPasswordHashAsync(int idPersona, string newHash)
    {
        // 🐾🐾 Buscamos la persona en la base de datos por su ID 🐾🐾
        var persona = await _context.personas.FirstOrDefaultAsync(p => p.id_persona == idPersona);
        if (persona == null) return;

        // 🐾🐾 Actualizamos el campo de contraseña con el nuevo hash 🐾🐾
        persona.contrasena = newHash;
        await _context.SaveChangesAsync();
    }

    // Falta implementar este metodo de creacion de usuario
}