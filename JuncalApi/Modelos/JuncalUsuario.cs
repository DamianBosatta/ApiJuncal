using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalUsuario
{
    public int Id { get; set; }

    public string Usuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public ulong PasswordHash { get; set; }

    public int? IdRol { get; set; }

    public bool Isdeleted { get; set; }

    public ulong PasswordSalt { get; set; }

    public virtual JuncalRole? IdRolNavigation { get; set; }
}
