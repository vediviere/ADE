using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADE.Seguridad.Domain.Entities;

namespace ADE.Seguridad.Application.Interfaces;


// 🐾 CAMINO DE MIGAJAS -- 3° TERCERA PARADA 🐾 Aquí se define el contrato. 🐾

// 🐾 El service no conoce la BD directamente
// 🐾 Se comunica por interfaz
// 🐾 Esto permite cambiar la implementación sin romper la lógica

// 🐾 CONTINUAMOS A LA CUARTA PARADA => UsuarioRepository 🐾

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByIdAsync(int id);
    Task<bool> ExisteEmailAsync(string email);
    Task<Usuario> CrearAsync(Usuario usuario);
    Task ActualizarPasswordHashAsync(int idPersona, string newHash);
}

public interface IJwtService
{
    string GenerarToken(Usuario usuario);
    bool ValidarToken(string token);
}
