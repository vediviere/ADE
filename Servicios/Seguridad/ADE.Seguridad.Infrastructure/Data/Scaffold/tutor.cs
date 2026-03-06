using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class tutor
{
    public int id_tutor { get; set; }

    public int id_persona { get; set; }

    public string nombreT { get; set; } = null!;

    public string a_paternoT { get; set; } = null!;

    public string a_maternoT { get; set; } = null!;

    public string ciudadT { get; set; } = null!;

    public string calleT { get; set; } = null!;

    public string coloniaT { get; set; } = null!;

    public string estadoT { get; set; } = null!;

    public string codigo_postalT { get; set; } = null!;

    public string telefonoT { get; set; } = null!;

    public virtual persona id_personaNavigation { get; set; } = null!;
}
