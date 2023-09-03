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
            CreateMap<TaskPriority, TaskPriorityDto>().IncludeMembers(t => t.User, t => t.Category);
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<TaskPriorityForCreationDto, TaskPriority>();
            CreateMap<TaskPriorityForUpdateDto, TaskPriority>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, TaskPriorityDto>();
            CreateMap<Category, TaskPriorityDto>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserIdentitiesDto>();
        }
    }
}
