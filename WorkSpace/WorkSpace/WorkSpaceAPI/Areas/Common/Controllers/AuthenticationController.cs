using BusinessAccessLayer.Abstraction;
using Common.Constants;
using Entities.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WorkSpaceAPI.Helpers;

namespace WorkSpaceAPI.Areas.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        #region Properties
        private readonly IAuthenticationService _authenticationService; 
        #endregion

        #region Cunstructor
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        #endregion

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var cookieHeaderValue = new SetCookieHeaderValue(SystemConstants.RememeberMeCookieKey, loginDto.RememberMe.ToString())
            {
                Expires = DateTime.UtcNow.AddDays(90),
                Path = "/", // Set the cookie path
                Domain = "localhost", // Set the cookie domain
                Secure = true,
                SameSite = Microsoft.Net.Http.Headers.SameSiteMode.None // Set whether the cookie requires a secure connection (https)
            };
            Response.Headers[HeaderNames.SetCookie] = cookieHeaderValue.ToString();
            return ResponseHelper.SuccessResponse(await _authenticationService.Login(loginDto), MessageConstants.MailSent);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(LoginOtpDto otpDto)
        {
            bool remeberMe = Request.Cookies[SystemConstants.RememeberMeCookieKey] is not null && Request.Cookies[SystemConstants.RememeberMeCookieKey] == SystemConstants.TrueString;
            return ResponseHelper.SuccessResponse(await _authenticationService.VerifyOtp(null,otpDto, remeberMe), MessageConstants.LoginSuccess);
        }

        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp(ResendOtpDto resendOtpDto )
        {
            await _authenticationService.SendOtp(null, resendOtpDto.Email, SystemConstants.AuthenticationOtp);
            return ResponseHelper.SuccessResponse(null, MessageConstants.MailSent);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(LoginEmailDto emailDto)
        {
            await _authenticationService.ForgotPassword(emailDto);
            return ResponseHelper.SuccessResponse(null,MessageConstants.MailSent);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto, string token)
        {
            await _authenticationService.ResetPassword(resetPasswordDto.Password,token);
            return ResponseHelper.SuccessResponse(null,MessageConstants.PasswordReset);
        }

        [HttpPost("refresh-jwttoken")]
        public async Task<IActionResult> RefreshToken(TokensDto tokensDto)
        {
            await _authenticationService.RefreshToken(tokensDto);
            return ResponseHelper.SuccessResponse(null,String.Empty);
        }

    }
}
