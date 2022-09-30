
using Core.Persistence.Repositories;
using Core.Security.Entities;
using IdentityService.Application.Services.Repositories;
using IdentityService.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Persistence.Repositories
{

    public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            //var result= new List<OperationClaim>();



            //using (var context = this.Context)
            //{
            var result = from operationClaim in this.Context.OperationClaims.ToList()
                         join userOperationClaim in this.Context.UserOperationClaims.ToList()
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

            //}

            return result.ToList();
        }
    
    }
   
}
