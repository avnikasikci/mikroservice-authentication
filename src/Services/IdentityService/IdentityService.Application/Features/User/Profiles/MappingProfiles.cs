using AutoMapper;
using Core.Persistence.Paging;
using IdentityService.Application.Features.User.Dto;
using IdentityService.Application.Features.User.Models;


namespace IdentityService.Application.Features.User.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<User, CreatedUserDto>().ReverseMap();
            //CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<IPaginate<Core.Security.Entities.User>, UserListModel>().ReverseMap();
            CreateMap<Core.Security.Entities.User, UserListDto>().ReverseMap();
            //CreateMap<Core.Security.Entities.User, UserGetByIdDto>().ReverseMap();
        }
    }
}
