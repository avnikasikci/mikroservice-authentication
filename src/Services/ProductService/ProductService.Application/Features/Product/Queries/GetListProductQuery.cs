using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using ProductService.Application.Features.User.Dto;
using ProductService.Application.Features.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Features.Product.Queries
{

    public class GetListProductQuery : IRequest<ProductListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }

        public string[] Roles
        {
            get
            {
                string[] result = new string[] { "ProductOwnerRole" };
                return result;
            }
            set { }
        }

        public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, ProductListModel>
        {
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public async Task<ProductListModel> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                var products = new ProductListModel();
                products.Items = new List<ProductListDto>() {
                    new ProductListDto() { Id=1,Name="Product1"} ,
                    new ProductListDto() { Id=2,Name="Product2"} ,
                    new ProductListDto() { Id=3,Name="Product3"} ,
                };


//                ProductListModel mappedBrandListModel = _mapper.Map<ProductListModel>(products);

                return products;
            }
        }
    }

}
