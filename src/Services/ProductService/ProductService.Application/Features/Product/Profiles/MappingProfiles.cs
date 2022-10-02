using AutoMapper;
using Core.Persistence.Paging;
using ProductService.Application.Features.User.Dto;
using ProductService.Application.Features.User.Models;

namespace ProductService.Application.Features.User.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<IPaginate<Core.Security.Entities.User>, ProductListModel>().ReverseMap();
            CreateMap<Core.Security.Entities.User, ProductListDto>().ReverseMap();
        }
    }
}
