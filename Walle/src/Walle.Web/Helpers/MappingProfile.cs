using AutoMapper;
using Walle.Core.Dtos;
using Walle.Infrastructure.Authentication;

namespace Walle.Web.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
