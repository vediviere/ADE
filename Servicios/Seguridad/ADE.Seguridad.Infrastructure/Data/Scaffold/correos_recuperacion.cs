using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class correos_recuperacion
{
    public int id_correo_recovery { get; set; }

    public string email { get; set; } = null!;

    public string clave { get; set; } = null!;

    public DateTime? fecha { get; set; }
}
