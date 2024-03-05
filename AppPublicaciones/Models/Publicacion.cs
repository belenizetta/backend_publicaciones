using System;
using System.Collections.Generic;

namespace AppPublicaciones.Models;

public partial class Publicacion
{
    public string? TipoPropiedad { get; set; }

    public string? TipoOperacion { get; set; }

    public string? Descripcion { get; set; }

    public int? Ambientes { get; set; }

    public decimal? M2 { get; set; }

    public int? Antiguedad { get; set; }

    public string? ListaImagenes { get; set; }

    public int Id { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }
}
