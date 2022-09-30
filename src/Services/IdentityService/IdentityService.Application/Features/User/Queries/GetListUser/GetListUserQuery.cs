

using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using IdentityService.Application.Features.User.Models;
using IdentityService.Application.Services.Repositories;
using MediatR;

namespace IdentityService.Application.Features.User.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<UserListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }

        public string[] Roles
        {
            get
            {
                string[] result = new string[] { "Admin" };
                return result;
            }
            set { }
        }

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, UserListModel>
        {
            private readonly IUserRepository _brandRepository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Core.Security.Entities.User> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                UserListModel mappedBrandListModel = _mapper.Map<UserListModel>(brands);

                return mappedBrandListModel;
            }
        }
    }
}
