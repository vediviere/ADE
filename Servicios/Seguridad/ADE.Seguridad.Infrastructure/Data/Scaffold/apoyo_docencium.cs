using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class apoyo_docencium
{
    public int id_apoyo_docencia { get; set; }

    public string? observaciones { get; set; }

    public int? horas { get; set; }

    public DateTime autotimestamp { get; set; }

    public int id_actividad_apoyo { get; set; }

    public int? id_carga_academica { get; set; }

    public virtual ICollection<horario_apoyo> horario_apoyos { get; set; } = new List<horario_apoyo>();

    public virtual actividad_apoyo id_actividad_apoyoNavigation { get; set; } = null!;

    public virtual carga_academica? id_carga_academicaNavigation { get; set; }
}
