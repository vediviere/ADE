using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADE.Seguridad.Domain.Entities;

// ❔ Dudas Existenciales - Preguntar
public class Usuario
{
    public int Id { get; set; }
    public int IdPersona { get; set; }

    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public int IdRol { get; set; }

    public Rol? Rol { get; set; }
}
