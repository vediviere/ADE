using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class materias_asignada
{
    public int id_mate_asignada { get; set; }

    public int s_c { get; set; }

    public DateTime autotimestamp { get; set; }

    public int? id_carga_academica { get; set; }

    public int? id_asignatura { get; set; }

    public int id_grupo { get; set; }

    public virtual ICollection<horario> horarios { get; set; } = new List<horario>();

    public virtual asignatura? id_asignaturaNavigation { get; set; }

    public virtual carga_academica? id_carga_academicaNavigation { get; set; }

    public virtual grupo id_grupoNavigation { get; set; } = null!;

    public virtual ICollection<materias_cursar> materias_cursars { get; set; } = new List<materias_cursar>();
}
