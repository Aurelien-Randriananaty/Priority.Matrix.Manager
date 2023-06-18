using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Priority.Matrix.Manager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
