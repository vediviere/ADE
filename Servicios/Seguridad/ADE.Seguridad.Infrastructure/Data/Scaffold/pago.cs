using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class pago
{
    public int id_pagos { get; set; }

    public DateOnly fecha_vencimiento { get; set; }

    public string nombreArchivo { get; set; } = null!;

    public string statusValidacion { get; set; } = null!;

    public int id_estudiante { get; set; }

    public int id_DSA { get; set; }

    public int id_reinscripcion { get; set; }
}
