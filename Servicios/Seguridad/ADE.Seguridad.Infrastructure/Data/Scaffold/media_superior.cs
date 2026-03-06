using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class media_superior
{
    public int id_mediaSuperior { get; set; }

    public string subsistemaEducativoM { get; set; } = null!;

    public virtual ICollection<nombremediasuperior> nombremediasuperiors { get; set; } = new List<nombremediasuperior>();
}
