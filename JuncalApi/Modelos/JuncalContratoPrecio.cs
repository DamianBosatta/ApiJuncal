using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalContratoPrecio
{
    public int Id { get; set; }

    public int IdItem { get; set; }

    public int IdMaterialAceria { get; set; }

    public decimal? Precio { get; set; }

    public bool Isdeleted { get; set; }

    public virtual JuncalContratoItem IdItemNavigation { get; set; } = null!;

    public virtual JuncalAceriaMaterial IdMaterialAceriaNavigation { get; set; } = null!;
}
