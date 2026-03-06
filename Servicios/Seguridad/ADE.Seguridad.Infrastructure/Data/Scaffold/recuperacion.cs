using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class recuperacion
{
    public int id_recuperacion { get; set; }

    public string correo { get; set; } = null!;

    public string token { get; set; } = null!;

    public DateOnly fecha { get; set; }
}
