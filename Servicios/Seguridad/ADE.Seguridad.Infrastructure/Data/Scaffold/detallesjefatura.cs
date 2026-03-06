using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class detallesjefatura
{
    public int id_jefatura { get; set; }

    public int id_persona { get; set; }

    public int id_carrera { get; set; }

    public string nivel_estudios { get; set; } = null!;

    public virtual ICollection<carga_academica> carga_academicas { get; set; } = new List<carga_academica>();

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual persona id_personaNavigation { get; set; } = null!;

    public virtual ICollection<solicitud_re_inscripcion> solicitud_re_inscripcions { get; set; } = new List<solicitud_re_inscripcion>();
}
