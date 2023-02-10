using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Origen { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<JuncalMaterialProveedor> JuncalMaterialProveedors { get; } = new List<JuncalMaterialProveedor>();
}
