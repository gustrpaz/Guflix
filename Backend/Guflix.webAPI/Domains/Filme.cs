using System;
using System.Collections.Generic;

namespace Guflix.webAPI.Domains;

public partial class Filme
{
    public int IdFilme { get; set; }

    public string? NomeFilme { get; set; }

    public string? Genero { get; set; }
}
