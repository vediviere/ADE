using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class nombremediasuperior
{
    public int id_nombremedia_superior { get; set; }

    public int id_mediaSuperior { get; set; }

    public string nombreMediaSuperior1 { get; set; } = null!;

    public virtual media_superior id_mediaSuperiorNavigation { get; set; } = null!;

    public virtual ICollection<solicitud_inscripcion> solicitud_inscripcions { get; set; } = new List<solicitud_inscripcion>();
}
