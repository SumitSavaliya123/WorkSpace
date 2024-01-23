using Entities.DTOs.Request;

namespace BusinessAccessLayer.Abstraction

{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginDto loginDto);

        Task<TokensDto> VerifyOtp(long? id, LoginOtpDto loginOtpDto, bool rememberMe);
    }
}
