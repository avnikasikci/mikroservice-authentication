

using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace IdentityService.Application.Services.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken>, IRepository<RefreshToken>
    {

    }
}
