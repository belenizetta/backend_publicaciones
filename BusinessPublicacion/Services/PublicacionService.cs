using AutoMapper;
using BusinessPublicacion.DTOs;
using BusinessPublicacion.Interface;
using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPublicacion.Services
{
    public class PublicacionService : IPublicacionService
    {
        private readonly IMapper _mapper;
        private readonly IPublicacionData _publicacionData;
        private readonly ILogger<PublicacionService> _logger;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "publicaciones";
        private IMapper mapper;
        private IPublicacionData publicacionData;
        private ILogger logger;
        private IAlmacenadorArchivos arhivosLocal;

        public PublicacionService (IMapper mapper, IPublicacionData publicacionData, ILogger<PublicacionService> logger, IAlmacenadorArchivos almacenadorArchivos)
        {
            _mapper = mapper;
            _publicacionData = publicacionData;
            _logger = logger;
            _almacenadorArchivos = almacenadorArchivos;
        }

        public PublicacionService(IMapper mapper, IPublicacionData publicacionData, ILogger logger, IAlmacenadorArchivos arhivosLocal)
        {
            this.mapper = mapper;
            this.publicacionData = publicacionData;
            this.logger = logger;
            this.arhivosLocal = arhivosLocal;
        }

        public async Task<PublicacionResponse> AddPublicacion (PublicacionRequest publicacionDto)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            var publicacion = _mapper.Map<Publicacion>(publicacionDto);
            int Id = 0;
            try
            {
                if(publicacionDto.listaImagenes != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await publicacionDto.listaImagenes.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(publicacionDto.listaImagenes.FileName);
                        publicacion.ListaImagenes = await _almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                            publicacionDto.listaImagenes.ContentType);

                    }
                }
                Id = await _publicacionData.InsertPublicacion(publicacion);
                _logger.LogDebug("Se generó una nueva publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                publicacion.Id = Id;
                var publicacionRes = _mapper.Map<PublicacionResponse>(publicacion);
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

                return publicacionRes;

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado al insertar una publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error no esperado al insertar una publicacion ", ex);
            }
        }

        public async Task<PublicacionResponse> UpdatePublicacion (PublicacionRequest publicacionDto, int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            var publicacion = _mapper.Map<Publicacion>(publicacionDto);
            try
            {
                var publicacionActual = await GetPublicacionById(id);
                if (publicacionDto.listaImagenes != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await publicacionDto.listaImagenes.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(publicacionDto.listaImagenes.FileName);
                        publicacion.ListaImagenes = await _almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,
                            publicacionActual.ListaImagenes, publicacionDto.listaImagenes.ContentType);

                    }
                }
                var idRes = await _publicacionData.UpdatePublicacion(publicacion, id);
                publicacion.Id = id;
                _logger.LogDebug("Se actualizó una publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicacionRes = _mapper.Map<PublicacionResponse>(publicacion);
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

                return publicacionRes;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado al actualizar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error no esperado al actualizar una publicacion ", ex);
            }
        }

        public async Task<PublicacionResponse> DeletePublicacion (int id)
        {
            try
            {
                _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicacion = await _publicacionData.DeletePublicacion(id);
                _logger.LogDebug("Se eliminó una publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicacionRes = _mapper.Map<PublicacionResponse>(publicacion);
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

                return publicacionRes;

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado al eliminar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error no esperado al eliminar una publicacion ", ex);
            }
        }

        public async Task<List<Publicacion>> GetAllPublicaciones()
        {
            try
            {
                _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicaciones = await _publicacionData.GetAllPublicaciones();
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

                return publicaciones;

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error no esperado", ex);
            }
        }

        public async Task<PublicacionResponse> GetPublicacionById(int id)
        {
            try
            {
                _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicacion = await _publicacionData.GetPublicacionById(id);
                var publicacionRes = _mapper.Map<PublicacionResponse>(publicacion);
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

                return publicacionRes;

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado al buscar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error no esperado al buscar una publicacion ", ex);
            }
        }
    }
}
