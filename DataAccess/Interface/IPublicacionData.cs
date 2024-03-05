using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IPublicacionData
    {
        Task<int> InsertPublicacion(Publicacion publicacion);
        Task<int> UpdatePublicacion(Publicacion publicacion, int id); 
        Task<Publicacion> DeletePublicacion (int id);
        Task<List<Publicacion>> GetAllPublicaciones();
        Task<Publicacion> GetPublicacionById(int id);
    }
}
