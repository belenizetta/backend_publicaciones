using BusinessPublicacion.DTOs;
using BusinessPublicacion.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AppPublicaciones.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api")]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;
        private readonly ILogger<PublicacionController> _logger;
        public PublicacionController(IPublicacionService publicacionService, ILogger<PublicacionController> logger) 
        {
            _publicacionService = publicacionService;
            _logger = logger;
        }

        /// <summary>
        /// Guarda las publicaciones en la base de datos. Recibe la publicación en formato JSON a través del body.
        /// </summary>
        /// <param name="publicacionRequest"></param>
        /// <returns>Para los casos existosos retorna un Status 200 y la publicación agregada recientemente.</returns>
        /// <returns>Para los casos de error retorna un Status 400 con un mensaje descriptivo</returns>

        [HttpPost("crear")]
        public async Task<ActionResult> AddPublicacion([FromForm] PublicacionRequest publicacionRequest)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);

            try 
            {
                if (!publicacionRequest.IsEmpty())
                {
                    await _publicacionService.AddPublicacion(publicacionRequest);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return Ok(publicacionRequest);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza las publicaciones mediante el Id. Se debe enviar los datos a actualizar mediante
        /// el JSON del body.
        /// </summary>
        /// <param name="publicacionUpdate"></param>
        /// <param name="id"></param>
        /// <returns>En los casos exitosos retorna un Status 200 con la publicacion actualizada</returns>
        /// <returns>En los casos de error retorna un Status 400 con un mensaje descriptivo</returns>
        [HttpPut("editar/{id}")]
        public async Task<ActionResult> UpdatePublicacion([FromForm] PublicacionRequest publicacionUpdate, [FromRoute] int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                if(id != 0)
                {
                    await _publicacionService.UpdatePublicacion(publicacionUpdate, id);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return Ok(publicacionUpdate);
                }
                else
                { return BadRequest(); }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest();
            }

        }

        /// <summary>
        /// Elimina las publicaciones mediante el Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>En los casos exitosos retorna un Status 200 con la publicacion eliminada</returns>
        /// <returns>En los casos de error retorna un Status 400 con un mensaje descriptivo</returns>
        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> DeletePublicacion([FromRoute] int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                if (id != 0)
                {
                    var publicacionDelete = await _publicacionService.DeletePublicacion(id);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return Ok(publicacionDelete);
                }
                else
                { return BadRequest(); }

            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene la lista de todos las publicaciones almacenadas en la base de datos.
        /// </summary>
        /// <returns>En los casos exitosos retorna un Status 200 con una lista de publicaciones</returns>
        /// <returns>En los casos de error retorna un Status 400 con un mensaje descriptivo</returns>

        [HttpGet("publicaciones")]
        public async Task<ActionResult> GetAllPublicaciones()
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                var publicaciones = await _publicacionService.GetAllPublicaciones();
                _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Obitiene la publicaciones mediante el Id.
        /// </summary>
        /// <param name="publicacionUpdate"></param>
        /// <param name="id"></param>
        /// <returns>En los casos exitosos retorna un Status 200 con la publicacion </returns>
        /// <returns>En los casos de error retorna un Status 400 con un mensaje descriptivo</returns>
        [HttpGet("publicaciones/{id}")]
        public async Task<ActionResult> GetPublicacionById([FromRoute] int id)
        {
            _logger.LogInformation("Inicio metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
            try
            {
                if (id != 0)
                {
                    var publicacion = await _publicacionService.GetPublicacionById(id);
                    _logger.LogInformation("Fin metodo " + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name);
                    return Ok(publicacion);
                }
                else
                { return BadRequest(); }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error no esperado" + MethodBase.GetCurrentMethod().DeclaringType.Name + '.' + MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest();
            }

        }
    }
}
