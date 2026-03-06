using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class materias_cursar
{
    public int id_materiasCursadas { get; set; }

    public string tipo_curso { get; set; } = null!;

    public int id_re_inscripcion { get; set; }

    public int? id_mate_asignada { get; set; }

    public virtual materias_asignada? id_mate_asignadaNavigation { get; set; }

    public virtual solicitud_re_inscripcion id_re_inscripcionNavigation { get; set; } = null!;
}
