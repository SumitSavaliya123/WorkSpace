using DataAccessLayer.Abstraction;
using DataAccessLayer.Data;
using Entities.DataModels;

namespace DataAccessLayer.Implementation
{
    public class AuthenticationRepository :GenericRepository<User>,IAuthenticationRepository
    {
        #region Properties
        public new readonly AppDbContext _dbContext;
        #endregion Properties

        #region Constructor
        public AuthenticationRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion Constructor

    }
}
