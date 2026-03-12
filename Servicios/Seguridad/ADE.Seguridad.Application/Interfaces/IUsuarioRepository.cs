using ADE.Seguridad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 🐾 CAMINO DE MIGAJAS -- 3° TERCERA PARADA 🐾 Aquí se define el contrato. 🐾

// 🐾 El service no conoce la BD directamente
// 🐾 Se comunica por interfaz
// 🐾 Esto permite cambiar la implementación sin romper la lógica

// 🐾 CONTINUAMOS A LA CUARTA PARADA => UsuarioRepository 🐾

namespace ADE.Seguridad.Application.Interfaces
{
    /*🐾🐾🐾🐾🐾🐾
    ===========================================================================
    INTERFAZ: IUsuarioRepository

    Esta interfaz define las operaciones que se pueden realizar sobre
    los usuarios del sistema.

    Importante:
    Application solo conoce la interfaz.
    Infrastructure implementa la lógica real contra la base de datos.

    Implementación real:
    ADE.Seguridad.Infrastructure/Repositories/UsuarioRepository.cs

    Tabla real consultada en BD:
    adedb.persona
    ===========================================================================
🐾🐾🐾🐾🐾🐾🐾*/
    public interface IUsuarioRepository
    {
        //🐾🐾 Obtiene un usuario usando su correo institucional 🐾🐾
        Task<Usuario?> GetByEmailAsync(string email);

        //🐾🐾 Obtiene un usuario usando su Id 🐾🐾
        Task<Usuario?> GetByIdAsync(int id);

        //🐾🐾 Verifica si un correo ya existe en el sistema 🐾🐾
        Task<bool> ExisteEmailAsync(string email);

        //🐾🐾 Crea un nuevo usuario 🐾🐾
        Task<Usuario> CrearAsync(Usuario usuario);

        //🐾🐾 Actualiza el hash de la contraseña 🐾🐾
        Task ActualizarPasswordHashAsync(int idPersona, string newHash);
    }
}
