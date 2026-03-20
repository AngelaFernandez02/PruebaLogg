using System;
using System.Collections.Generic;

namespace PruebaLog.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public bool? Estatus { get; set; }
}
