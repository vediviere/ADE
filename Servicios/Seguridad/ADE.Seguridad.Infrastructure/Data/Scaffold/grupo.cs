using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class grupo
{
    public int id_grupo { get; set; }

    public string nombreClave { get; set; } = null!;

    public int id_carrera { get; set; }

    public string? token { get; set; }

    public virtual ICollection<detallesestudiante> detallesestudiantes { get; set; } = new List<detallesestudiante>();

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual ICollection<materias_asignada> materias_asignada { get; set; } = new List<materias_asignada>();

    public virtual ICollection<tutorgrupo> tutorgrupos { get; set; } = new List<tutorgrupo>();
}
