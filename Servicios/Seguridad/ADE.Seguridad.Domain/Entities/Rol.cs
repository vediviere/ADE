using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADE.Seguridad.Domain.Entities;
public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
