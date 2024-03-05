using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPublicacion.DTOs
{
    public class PublicacionRequest
    {
        public string? TipoPropiedad { get; set; }

        public string? TipoOperacion { get; set; }

        public string? Descripcion { get; set; }

        public int? Ambientes { get; set; }

        public decimal? M2 { get; set; }

        public int? Antiguedad { get; set; }

        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public IFormFile? listaImagenes { get; set; }


        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(TipoPropiedad)
                    && string.IsNullOrWhiteSpace(TipoOperacion)
                    && string.IsNullOrWhiteSpace(Descripcion)
                    && !Ambientes.HasValue
                    && !M2.HasValue
                    && !Antiguedad.HasValue
                    && !Latitud.HasValue
                    && !Longitud.HasValue;
        }
    }

    
}
