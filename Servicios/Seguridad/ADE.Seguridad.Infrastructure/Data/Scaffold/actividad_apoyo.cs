using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class actividad_apoyo
{
    public int id_actividad_apoyo { get; set; }

    public string nombre_actividad { get; set; } = null!;

    public virtual ICollection<apoyo_docencium> apoyo_docencia { get; set; } = new List<apoyo_docencium>();
}
