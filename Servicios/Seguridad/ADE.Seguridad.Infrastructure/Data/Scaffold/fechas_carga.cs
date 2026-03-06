using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class fechas_carga
{
    public int id_fechas_carga { get; set; }

    public DateOnly? fecha_planeacion { get; set; }

    public DateOnly? fecha_reporte1 { get; set; }

    public DateOnly? fecha_reporte2 { get; set; }

    public DateOnly? fecha_reporte3 { get; set; }

    public DateOnly? fecha_acta_final { get; set; }

    public DateOnly fecha_horas_apoyo { get; set; }

    public string? periodo_carga { get; set; }

    public DateOnly periodoICA { get; set; }

    public DateOnly periodoFCA { get; set; }

    public DateOnly FISRC { get; set; }

    public DateOnly FFSRC { get; set; }

    public DateTime autotimestamp { get; set; }

    public string? status_f { get; set; }

    public DateOnly? periodo_inicial { get; set; }

    public DateOnly? periodo_final { get; set; }

    public int? ano_reinscripcion { get; set; }

    public virtual ICollection<carga_academica> carga_academicas { get; set; } = new List<carga_academica>();

    public virtual ICollection<solicitud_re_inscripcion> solicitud_re_inscripcions { get; set; } = new List<solicitud_re_inscripcion>();
}
