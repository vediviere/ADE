using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class detallesadministrativo
{
    public int id_administrativos { get; set; }

    public string puesto { get; set; } = null!;

    public string nivel_estudios { get; set; } = null!;

    public int id_persona { get; set; }

    public virtual persona id_personaNavigation { get; set; } = null!;
}
