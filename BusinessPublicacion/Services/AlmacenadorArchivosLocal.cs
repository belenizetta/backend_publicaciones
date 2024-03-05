using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPublicacion.Services
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContext;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContext )
        {
            this.env = env;
            this.httpContext = httpContext;
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta !=null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorio = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);
                if (File.Exists(directorio))
                {
                    File.Delete(directorio);
                }
            }
                return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType) 
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contenType) 
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var urlActual = $"{httpContext.HttpContext.Request.Scheme}://{httpContext.HttpContext.Request.Host}";
            var urlBd = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\","/");
            return urlBd;
        }
    }
}
