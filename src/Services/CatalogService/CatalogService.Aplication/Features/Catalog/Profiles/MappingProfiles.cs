
using AutoMapper;
using CatalogService.Application.Features.Catalog.Models;
using CatalogService.Application.Features.User.Dto;

namespace CatalogService.Application.Features.Catalog.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            //CreateMap<IPaginate<Core.Security.Entities.User>, CatalogListModel>().ReverseMap();
            //CreateMap<Core.Security.Entities.User, CatalogListDto>().ReverseMap();
        }
    }
}
