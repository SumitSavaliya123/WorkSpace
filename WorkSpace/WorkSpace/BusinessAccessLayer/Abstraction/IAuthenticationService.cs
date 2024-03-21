using Entities.DataModels;
using Entities.DTOs.Request;

namespace BusinessAccessLayer.Abstraction

{
    public interface IAuthenticationService
    {
        Task<string> RegisterUser(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);

        Task<string> SocialMediaLogin(string email);

        Task SendOtp(long? id, string email, string typeOfOtp);

        Task<TokensDto> VerifyOtp(long? id, LoginOtpDto loginOtpDto, bool rememberMe);

        Task ForgotPassword(LoginEmailDto emailDto);

        Task ResetPassword(string password, string token);

        Task<TokensDto> RefreshToken(TokensDto tokenDto);
    }
}
