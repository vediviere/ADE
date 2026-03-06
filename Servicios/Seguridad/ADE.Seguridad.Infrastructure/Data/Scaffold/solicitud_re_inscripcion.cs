using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class solicitud_re_inscripcion
{
    public int id_re_inscripcion { get; set; }

    public DateOnly fecha_registro { get; set; }

    public string turno { get; set; } = null!;

    public string semestre_re_inscripcion { get; set; } = null!;

    public string status_SR { get; set; } = null!;

    public string status_inscripcion { get; set; } = null!;

    public int id_academicos { get; set; }

    public int id_fechas_carga { get; set; }

    public string token { get; set; } = null!;

    public int id_carrera { get; set; }

    public int id_jefatura { get; set; }

    public virtual detallesestudiante id_academicosNavigation { get; set; } = null!;

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual fechas_carga id_fechas_cargaNavigation { get; set; } = null!;

    public virtual detallesjefatura id_jefaturaNavigation { get; set; } = null!;

    public virtual ICollection<materias_cursar> materias_cursars { get; set; } = new List<materias_cursar>();
}
