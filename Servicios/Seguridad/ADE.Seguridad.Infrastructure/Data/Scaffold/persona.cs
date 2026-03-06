using System;
using System.Collections.Generic;

namespace ADE.Seguridad.Infrastructure.Data.Scaffold;

public partial class persona
{
    public int id_persona { get; set; }

    public string nombre { get; set; } = null!;

    public string a_paterno { get; set; } = null!;

    public string a_materno { get; set; } = null!;

    public DateOnly? fechaNcimiento { get; set; }

    public string estadoCivil { get; set; } = null!;

    public string estado { get; set; } = null!;

    public string municipio { get; set; } = null!;

    public string ciudad { get; set; } = null!;

    public string colonia { get; set; } = null!;

    public string calle { get; set; } = null!;

    public int cp { get; set; }

    public int n_exterior { get; set; }

    public int n_interior { get; set; }

    public string telefono { get; set; } = null!;

    public string correo_inst { get; set; } = null!;

    public string contrasena { get; set; } = null!;

    public string curp { get; set; } = null!;

    public string rfc { get; set; } = null!;

    public int intentos { get; set; }

    public string observacion { get; set; } = null!;

    public string status { get; set; } = null!;

    public int id_rol { get; set; }

    public int ADE_setup { get; set; }

    public virtual ICollection<detallesadministrativo> detallesadministrativos { get; set; } = new List<detallesadministrativo>();

    public virtual ICollection<detallesdocente> detallesdocentes { get; set; } = new List<detallesdocente>();

    public virtual ICollection<detallesestudiante> detallesestudiantes { get; set; } = new List<detallesestudiante>();

    public virtual ICollection<detallesjefatura> detallesjefaturas { get; set; } = new List<detallesjefatura>();

    public virtual ICollection<notificacione> notificacioneid_destinatarioNavigations { get; set; } = new List<notificacione>();

    public virtual ICollection<notificacione> notificacioneid_remitenteNavigations { get; set; } = new List<notificacione>();

    public virtual ICollection<tutor> tutors { get; set; } = new List<tutor>();
}
