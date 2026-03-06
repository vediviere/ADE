using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class detallesdocente
{
    public int id_docente { get; set; }

    public int horas { get; set; }

    public string tipo_clave { get; set; } = null!;

    public string nivel_estudios { get; set; } = null!;

    public int id_persona { get; set; }

    public int id_carrera { get; set; }

    public virtual ICollection<carga_academica> carga_academicas { get; set; } = new List<carga_academica>();

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual persona id_personaNavigation { get; set; } = null!;
}
