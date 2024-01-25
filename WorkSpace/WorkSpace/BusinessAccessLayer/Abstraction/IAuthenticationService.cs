using Entities.DTOs.Request;

namespace BusinessAccessLayer.Abstraction

{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDto loginDto);

        Task SendOtp(long? id, string email, string typeOfOtp);

        Task<TokensDto> VerifyOtp(long? id, LoginOtpDto loginOtpDto, bool rememberMe);

        Task ForgotPassword(LoginEmailDto emailDto);

        Task ResetPassword(string password, string token);
    }
}
