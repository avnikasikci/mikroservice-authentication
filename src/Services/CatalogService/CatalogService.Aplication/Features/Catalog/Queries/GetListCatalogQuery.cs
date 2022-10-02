using AutoMapper;
using CatalogService.Application.Features.Catalog.Models;
using CatalogService.Application.Features.User.Dto;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace CatalogService.Application.Features.Catalog.Queries
{

    public class GetListCatalogQuery : IRequest<CatalogListModel>, ISecuredRequest
    {

        public string[] Roles
        {
            get
            {
                string[] result = new string[] { "CatalogOwnerRole" }; // ProductOwnerRole
                return result;
            }
            set { }
        }

        public class GetListCatalogQueryHandler : IRequestHandler<GetListCatalogQuery, CatalogListModel>
        {
            private readonly IMapper _mapper;

            public GetListCatalogQueryHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public async Task<CatalogListModel> Handle(GetListCatalogQuery request, CancellationToken cancellationToken)
            {
                var catalogs = new CatalogListModel();
                catalogs.Items = new List<CatalogListDto>() {
                    new CatalogListDto() { Id=1,Name="Product1"} ,
                    new CatalogListDto() { Id=2,Name="Product2"} ,
                    new CatalogListDto() { Id=3,Name="Product3"} ,
                };


                //CatalogListModel mappedBrandListModel = _mapper.Map<CatalogListModel>(products);

                return catalogs;
            }
        }
    }

}
