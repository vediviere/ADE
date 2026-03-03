using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADE.Seguridad.Domain.Entities;

namespace ADE.Seguridad.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByIdAsync(int id);
    Task<bool> ExisteEmailAsync(string email);
    Task<Usuario> CrearAsync(Usuario usuario);
}

public interface IJwtService
{
    string GenerarToken(Usuario usuario);
    bool ValidarToken(string token);
}
