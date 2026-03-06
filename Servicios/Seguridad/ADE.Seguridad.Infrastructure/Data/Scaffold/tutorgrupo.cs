using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class tutorgrupo
{
    public int id_tutorgrupo { get; set; }

    public int? id_carga_academica { get; set; }

    public int id_grupo { get; set; }

    public int horas { get; set; }

    public virtual ICollection<horario_tutoria> horario_tutoria { get; set; } = new List<horario_tutoria>();

    public virtual carga_academica? id_carga_academicaNavigation { get; set; }

    public virtual grupo id_grupoNavigation { get; set; } = null!;
}
