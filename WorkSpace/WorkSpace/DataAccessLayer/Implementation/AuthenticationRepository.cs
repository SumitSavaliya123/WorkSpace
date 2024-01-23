using DataAccessLayer.Abstraction;
using DataAccessLayer.Data;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

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

        #region Methods

        public async Task<User> GetUserByEmail(string email) => await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

        public async Task<UserRefreshTokens> AddUserRefreshToken(UserRefreshTokens userRefreshTokens)
        {
            await _dbContext.UserRefreshTokens.AddAsync(userRefreshTokens);
            return userRefreshTokens;
        }
        #endregion
    }
}
