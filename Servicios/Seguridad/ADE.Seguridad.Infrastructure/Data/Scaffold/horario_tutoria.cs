using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class horario_tutoria
{
    public int id_horario_tutoria { get; set; }

    public string? dia_tutoria { get; set; }

    public string? h_apoyo_inicial { get; set; }

    public string? h_apoyo_final { get; set; }

    public int? id_aula { get; set; }

    public int? id_tutorgrupo { get; set; }

    public virtual aula? id_aulaNavigation { get; set; }

    public virtual tutorgrupo? id_tutorgrupoNavigation { get; set; }
}
