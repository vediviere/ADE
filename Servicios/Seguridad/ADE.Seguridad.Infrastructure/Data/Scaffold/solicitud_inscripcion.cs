using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class solicitud_inscripcion
{
    public int id_solicitud_inscripcion { get; set; }

    public DateOnly fecha_inscripcion { get; set; }

    public string promedio_mediasup { get; set; } = null!;

    public int id_nombremedia_superior { get; set; }

    public int id_academicos { get; set; }

    public string token { get; set; } = null!;

    public virtual detallesestudiante id_academicosNavigation { get; set; } = null!;

    public virtual nombremediasuperior id_nombremedia_superiorNavigation { get; set; } = null!;
}
