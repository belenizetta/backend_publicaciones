using AutoMapper;
using BusinessPublicacion.DTOs;
using DataAccess.Models;

namespace BusinessPublicacion.Mapper
{
    public class PublicacionMapping : Profile
    {
        public PublicacionMapping() 
        {
            CreateMap<PublicacionRequest, Publicacion>();
            CreateMap<Publicacion, PublicacionRequest>();
            CreateMap<Publicacion, PublicacionResponse>();
        }
    }
}
