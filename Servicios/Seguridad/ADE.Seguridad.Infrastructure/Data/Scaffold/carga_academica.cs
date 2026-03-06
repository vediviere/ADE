using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class carga_academica
{
    public int id_carga_academica { get; set; }

    public string? no_oficio { get; set; }

    public int? total_horas_frente_grupo { get; set; }

    public int? total_horas_apoyo { get; set; }

    public DateOnly? fecha_asignacion_carga { get; set; }

    public DateTime autotimestamp { get; set; }

    public int? id_docente { get; set; }

    public int? id_jefatura { get; set; }

    public int? id_fechas_carga { get; set; }

    public string token { get; set; } = null!;

    public virtual ICollection<apoyo_docencium> apoyo_docencia { get; set; } = new List<apoyo_docencium>();

    public virtual detallesdocente? id_docenteNavigation { get; set; }

    public virtual fechas_carga? id_fechas_cargaNavigation { get; set; }

    public virtual detallesjefatura? id_jefaturaNavigation { get; set; }

    public virtual ICollection<materias_asignada> materias_asignada { get; set; } = new List<materias_asignada>();

    public virtual ICollection<tutorgrupo> tutorgrupos { get; set; } = new List<tutorgrupo>();
}
