using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class carrera
{
    public int id_carrera { get; set; }

    public string clave_estudios { get; set; } = null!;

    public string nivel_estudios { get; set; } = null!;

    public string nombreCarrera { get; set; } = null!;

    public string abreviatura { get; set; } = null!;

    public string modalidad { get; set; } = null!;

    public virtual ICollection<asignatura> asignaturas { get; set; } = new List<asignatura>();

    public virtual ICollection<detallesdocente> detallesdocentes { get; set; } = new List<detallesdocente>();

    public virtual ICollection<detallesestudiante> detallesestudiantes { get; set; } = new List<detallesestudiante>();

    public virtual ICollection<detallesjefatura> detallesjefaturas { get; set; } = new List<detallesjefatura>();

    public virtual ICollection<grupo> grupos { get; set; } = new List<grupo>();

    public virtual ICollection<solicitud_re_inscripcion> solicitud_re_inscripcions { get; set; } = new List<solicitud_re_inscripcion>();
}
