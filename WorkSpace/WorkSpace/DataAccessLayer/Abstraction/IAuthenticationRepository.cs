using Entities.DataModels;

namespace DataAccessLayer.Abstraction
{
    public interface IAuthenticationRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);

        Task<UserRefreshTokens> AddUserRefreshToken(UserRefreshTokens userRefreshTokens);
    }
}
