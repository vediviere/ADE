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
    /* 🐾🐾🐾🐾🐾🐾
   ===========================================================================
   INTERFAZ: IJwtService

   Define el contrato para la generación y validación de tokens JWT.

   El token JWT es utilizado para autenticar usuarios en el sistema
   después de realizar login.

   Flujo:

   Login
     ↓
   AuthService
     ↓
   IJwtService
     ↓
   JwtService (Infrastructure)
     ↓
   Generación de Token
     ↓
   Cliente recibe JWT
     ↓
   Cliente usa JWT en Authorization Header
   ===========================================================================
   🐾🐾🐾🐾🐾🐾*/
    public interface IJwtService
    {
        //🐾🐾 Genera un token JWT a partir de los datos del usuario 🐾🐾
        string GenerarToken(Usuario usuario);

        //🐾🐾 Valida si un token JWT es válido 🐾🐾
        bool ValidarToken(string token);
    }
}
