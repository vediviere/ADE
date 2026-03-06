using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class horario
{
    public int id_horario { get; set; }

    public string dia { get; set; } = null!;

    public string hora_inicial { get; set; } = null!;

    public string hora_final { get; set; } = null!;

    public int id_aula { get; set; }

    public int id_mate_asignatura { get; set; }

    public virtual aula id_aulaNavigation { get; set; } = null!;

    public virtual materias_asignada id_mate_asignaturaNavigation { get; set; } = null!;
}
