using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalOrden
{
    public int Id { get; set; }

    public int IdAceria { get; set; }

    public int? IdContrato { get; set; }

    public string Remito { get; set; } = null!;

    public int? IdCamion { get; set; }

    public int IdEstado { get; set; }

    public DateOnly Fecha { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual JuncalAcerium IdAceriaNavigation { get; set; } = null!;

    public virtual JuncalCamion? IdCamionNavigation { get; set; }

    public virtual JuncalContrato? IdContratoNavigation { get; set; }

    public virtual JuncalEstado IdEstadoNavigation { get; set; } = null!;

    public virtual ICollection<JuncalOrdenMarterial> JuncalOrdenMarterials { get; } = new List<JuncalOrdenMarterial>();
}
