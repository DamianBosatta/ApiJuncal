using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalContratoItem
{
    public int Id { get; set; }

    public int IdContrato { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual JuncalContrato IdContratoNavigation { get; set; } = null!;

    public virtual ICollection<JuncalContratoPrecio> JuncalContratoPrecios { get; } = new List<JuncalContratoPrecio>();
}
