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
            return ResponseHelper.SuccessResponse(await _authenticationService.Login(loginDto), MessageConstants.GlobalSuccess);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(LoginOtpDto otpDto)
        {
            bool remeberMe = Request.Cookies[SystemConstants.RememeberMeCookieKey] is not null && Request.Cookies[SystemConstants.RememeberMeCookieKey] == SystemConstants.TrueString;
            return ResponseHelper.SuccessResponse(await _authenticationService.VerifyOtp(null,otpDto, remeberMe), MessageConstants.LoginSuccess);
        }

    }
}
