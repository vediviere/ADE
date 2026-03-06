using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class notificacione
{
    public int id_notificacion { get; set; }

    public string titulo { get; set; } = null!;

    public string descripcion { get; set; } = null!;

    public int id_remitente { get; set; }

    public int id_destinatario { get; set; }

    public string token { get; set; } = null!;

    public string status { get; set; } = null!;

    public DateTime fecha_envio { get; set; }

    public virtual persona id_destinatarioNavigation { get; set; } = null!;

    public virtual persona id_remitenteNavigation { get; set; } = null!;
}
