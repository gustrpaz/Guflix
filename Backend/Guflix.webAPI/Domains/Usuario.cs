using System;
using System.Collections.Generic;

namespace Guflix.webAPI.Domains;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwd { get; set; } = null!;
}
