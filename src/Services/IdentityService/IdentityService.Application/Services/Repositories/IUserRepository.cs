

using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace IdentityService.Application.Services.Repositories
{

    public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
    {

        List<OperationClaim> GetClaims(User user);

    }
}
