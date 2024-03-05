using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations
{
    public class PublicacionData : IPublicacionData
    {
        private readonly PublicacionContext _publicacionContext;
        private readonly ILogger<PublicacionData> _logger;

        public PublicacionData(PublicacionContext publicacionContext, ILogger<PublicacionData> logger)
        {
            _publicacionContext = publicacionContext;
            _logger = logger;
        }


        public async Task<int> InsertPublicacion(Publicacion publicacion)
        {
            try
            {
                _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                
                _publicacionContext.Publicacions.Add(publicacion);
                await _publicacionContext.SaveChangesAsync();

                _logger.LogDebug("Se generó una nueva publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                
                return publicacion.Id;
            }catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error con la Base de Datos al insertar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error con la Base de Datos al insertar un publicacion ",ex);
            }
        }

        public async Task<int>UpdatePublicacion(Publicacion publicacion, int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                var publicacionUpdate = await _publicacionContext.Publicacions.FindAsync(id);

                if(publicacionUpdate != null)
                {
                    publicacionUpdate.Descripcion = publicacion.Descripcion;
                    publicacionUpdate.TipoOperacion = publicacion.TipoOperacion;
                    publicacionUpdate.Ambientes = publicacion.Ambientes;
                    publicacionUpdate.Antiguedad = publicacion.Antiguedad;
                    publicacionUpdate.TipoPropiedad = publicacion.TipoPropiedad;
                    publicacionUpdate.Latitud = publicacion.Latitud;
                    publicacionUpdate.Longitud = publicacion.Longitud;
                    publicacionUpdate.ListaImagenes = publicacion.ListaImagenes;
                    publicacionUpdate.M2 = publicacion.M2;
                    await _publicacionContext.SaveChangesAsync();
                    _logger.LogDebug("Se modificó una publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return publicacion.Id;
                }
                else
                {
                    _logger.LogError("El id ingresado no se encuentra en la Base de Datos" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    throw new KeyNotFoundException("El id ingresado no se encuentra en la Base de Datos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error con la Base de Datos al modificar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error con la Base de Datos al modificar un publicacion ", ex);
            }
        }

        public async Task<Publicacion> DeletePublicacion(int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                var publicacionDelete = await _publicacionContext.Publicacions.FindAsync(id);
                if(publicacionDelete != null)
                {
                    _publicacionContext.Publicacions.Remove(publicacionDelete);
                    await _publicacionContext.SaveChangesAsync();
                    _logger.LogDebug("Se eliminó una nueva publicacion " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return publicacionDelete;
                }
                else
                {
                    _logger.LogError("El id ingresado no se encuentra en la Base de Datos" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    throw new KeyNotFoundException("El id ingresado no se encuentra en la Base de Datos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error con la Base de Datos al eliminar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error con la Base de Datos al eliminar un publicacion ", ex);
            }
        }

        public async Task<List<Publicacion>> GetAllPublicaciones()
        {
            try
            {
                _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                var publicaciones =  _publicacionContext.Publicacions.ToList();
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                return publicaciones;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error con la Base de Datos " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error con la Base de Datos ", ex);
            }
        }

        public async Task<Publicacion> GetPublicacionById(int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                var publicacion = await _publicacionContext.Publicacions.FindAsync(id);
                if (publicacion != null)
                {
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return publicacion;
                }
                else
                {
                    _logger.LogError("El id ingresado no se encuentra en la Base de Datos" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    throw new KeyNotFoundException("El id ingresado no se encuentra en la Base de Datos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió un error con la Base de Datos al buscar una publicacion" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                throw new Exception("Ocurrio un error con la Base de Datos al buscar un publicacion ", ex);
            }
        }

    }
}
