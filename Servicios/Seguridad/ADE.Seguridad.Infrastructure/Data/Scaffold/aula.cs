using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class aula
{
    public int id_aula { get; set; }

    public string nombre_aula { get; set; } = null!;

    public int capacidad { get; set; }

    public virtual ICollection<horario_apoyo> horario_apoyos { get; set; } = new List<horario_apoyo>();

    public virtual ICollection<horario_tutoria> horario_tutoria { get; set; } = new List<horario_tutoria>();

    public virtual ICollection<horario> horarios { get; set; } = new List<horario>();
}
