using BusinessPublicacion.DTOs;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPublicacion.Interface
{
    public interface IPublicacionService
    {
        Task<PublicacionResponse> AddPublicacion (PublicacionRequest publicacion);
        Task<PublicacionResponse> UpdatePublicacion(PublicacionRequest publicacion, int id);
        Task<PublicacionResponse> DeletePublicacion(int id);
        Task<List<Publicacion>> GetAllPublicaciones();
        Task<PublicacionResponse> GetPublicacionById(int id);

    }
}
