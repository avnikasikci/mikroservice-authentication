using Core.Security.Entities;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }


        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string password = "123456";
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            string password2 = "123456";
            byte[] passwordHash2, passwordSalt2;
            HashingHelper.CreatePasswordHash(password2, out passwordHash2, out passwordSalt2);

            string password3 = "123456";
            byte[] passwordHash3, passwordSalt3;
            HashingHelper.CreatePasswordHash(password3, out passwordHash3, out passwordSalt3);

            User[] userEntitySeeds = {
                new(1,"test-admint","testLast","test@gmail.com", passwordSalt, passwordHash,true,0),
                new(2, "testCatalowOwner", "testCatalowOwnerlast", "test-catalog@gmail.com", passwordSalt2, passwordHash2, true, 0),
                new(3, "testProductOwner", "testProductOwnerLast", "test-product@gmail.com", passwordSalt3, passwordHash3, true, 0)

            };
            modelBuilder.Entity<User>().HasData(userEntitySeeds);

            OperationClaim[] operationClaimEntitySeeds = { new(1, "Admin"), new(2, "ProductOwnerRole"), new(3, "CatalogOwnerRole") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);

            UserOperationClaim[] userOperationClaimEntitySeeds = { new(1, 1, 1), new(2, 2, 2), new(3, 3, 3) };
            modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaimEntitySeeds);



        }
    }

}
