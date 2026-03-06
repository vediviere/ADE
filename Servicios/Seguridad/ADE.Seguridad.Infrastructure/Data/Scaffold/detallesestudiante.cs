using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class detallesestudiante
{
    public int id_academicos { get; set; }

    public string matricula { get; set; } = null!;

    public string semestre { get; set; } = null!;

    public string generacion { get; set; } = null!;

    public int id_grupo { get; set; }

    public int id_carrera { get; set; }

    public int id_persona { get; set; }

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual grupo id_grupoNavigation { get; set; } = null!;

    public virtual persona id_personaNavigation { get; set; } = null!;

    public virtual ICollection<solicitud_inscripcion> solicitud_inscripcions { get; set; } = new List<solicitud_inscripcion>();

    public virtual ICollection<solicitud_re_inscripcion> solicitud_re_inscripcions { get; set; } = new List<solicitud_re_inscripcion>();
}
