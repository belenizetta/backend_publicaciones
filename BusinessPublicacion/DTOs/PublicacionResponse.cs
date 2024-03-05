using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPublicacion.DTOs
{
    public class PublicacionResponse
    {
        public string? TipoPropiedad { get; set; }

        public string? TipoOperacion { get; set; }

        public string? Descripcion { get; set; }

        public int? Ambientes { get; set; }

        public decimal? M2 { get; set; }

        public int? Antiguedad { get; set; }

        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public string? ListaImagenes { get; set; }

        public int Id { get; set; }
    }
}
