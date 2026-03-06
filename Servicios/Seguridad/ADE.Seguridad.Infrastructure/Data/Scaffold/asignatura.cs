using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class asignatura
{
    public int id_asignatura { get; set; }

    public string? clv_asignatura { get; set; }

    public string? nombreMat { get; set; }

    public int? h_teoricas { get; set; }

    public int? h_practicas { get; set; }

    public int? creditos { get; set; }

    public string? semestremateria { get; set; }

    public DateTime autotimestamp { get; set; }

    public int id_carrera { get; set; }

    public virtual carrera id_carreraNavigation { get; set; } = null!;

    public virtual ICollection<materias_asignada> materias_asignada { get; set; } = new List<materias_asignada>();
}
