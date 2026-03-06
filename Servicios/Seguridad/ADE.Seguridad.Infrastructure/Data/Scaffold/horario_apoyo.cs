using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class horario_apoyo
{
    public int id_horario_apoyo { get; set; }

    public string? dia_apoyo { get; set; }

    public string? h_apoyo_inicial { get; set; }

    public string? h_apoyo_final { get; set; }

    public int? id_aula { get; set; }

    public int? id_apoyo_docencia { get; set; }

    public virtual apoyo_docencium? id_apoyo_docenciaNavigation { get; set; }

    public virtual aula? id_aulaNavigation { get; set; }
}
