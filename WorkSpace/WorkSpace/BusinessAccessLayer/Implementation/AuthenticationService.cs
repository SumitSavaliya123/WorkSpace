using BusinessAccessLayer.Abstraction;
using Common.Constants;
using Common.Exceptions;
using Common.Utils;
using DataAccessLayer.Abstraction;
using Entities.DataModels;
using Entities.DTOs.Request;
using System.Security.Claims;

namespace BusinessAccessLayer.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IJwtManageService _jwtManageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        #endregion

        #region Cunstructor
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IJwtManageService jwtManageService, IUnitOfWork unitOfWork, IMailService mailService)
        {
            _authenticationRepository = authenticationRepository;
            _jwtManageService = jwtManageService;
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }
        #endregion

        #region Methods

        public async Task<string> Login(LoginDto loginDto)
        {
            User? user = await _authenticationRepository.GetFirstOrDefaultAsync(user => user.Email == loginDto.Email && user.Password == loginDto.Password);
            if (user == null) throw new ModelValidationException(MessageConstants.InvalidLoginCredential);
            await SendOtp(null, user.Email, SystemConstants.AuthenticationOtp);
            return user.FirstName;
        }

        public async Task<string> SocialMediaLogin(string email)
        {
            User? user = await _authenticationRepository.GetFirstOrDefaultAsync(user => user.Email == email);
            if (user == null) throw new ModelValidationException(MessageConstants.InvalidLoginCredential);
            await SendOtp(null, email, SystemConstants.AuthenticationOtp);
            return user.FirstName;
        }

        public async Task SendOtp(long? id, string email, string typeOfOtp)
        {
            User user = id.HasValue ? await _authenticationRepository.GetByIdAsync(id.Value) : await _authenticationRepository.GetUserByEmail(email);
            if(id.HasValue) throw new ModelValidationException(MessageConstants.EmailAlreadyExists);

            Random generator = new();
            user.OTP = generator.Next(100000, 999999).ToString();
            user.ExpiryTime = DateTime.Now.AddMinutes(10);

            await _authenticationRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            string emailBody = typeOfOtp == SystemConstants.AuthenticationOtp ?
                MailBodyUtil.SendOtpForAuthenticationBody(user.OTP) :
                MailBodyUtil.SendOtpForProfileBody(user.OTP);

            await _mailService.SendMailAsync(new MailDto
            {
                ToEmail = email,
                Subject = MailConstants.OtpSubject,
                Body = emailBody
            });

        }

        public async Task<TokensDto> VerifyOtp(long? id, LoginOtpDto loginOtpDto, bool rememberMe)
        {
            User user = (id.HasValue ? await _authenticationRepository.GetByIdAsync(id.Value) : await _authenticationRepository.GetUserByEmail(loginOtpDto.Email) ?? throw new ModelValidationException(MessageConstants.DEFAULT_MODELSTATE));

            if (user.OTP != loginOtpDto.Otp || user.ExpiryTime < DateTime.Now) throw new ModelValidationException(MessageConstants.Invalidotp);
            user.OTP = null;
            user.ExpiryTime = null;
            await _authenticationRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            TokensDto token = _jwtManageService.GenerateToken(user) ?? throw new ModelValidationException(MessageConstants.INVALID_ATTEMPT);
            if (rememberMe)
            {
                UserRefreshTokens userRefreshTokens = new()
                {
                    RefreshToken = token.RefreshToken,
                    Email = user.Email,
                };
                await _authenticationRepository.AddUserRefreshToken(userRefreshTokens);
                await _unitOfWork.SaveAsync();
            }
            return token;
        }

        public async Task ForgotPassword(LoginEmailDto emailDto)
        {
            User user = await _authenticationRepository.GetUserByEmail(emailDto.Email);
            if (user != null)
            {
                MailDto mailDto = new()
                {
                    ToEmail = emailDto.Email,
                    Body = MailBodyUtil.SendResetPasswordLink("http://localhost:4200/reset-password?token=" + EncodingMailToken(emailDto.Email)),
                    Subject = MailConstants.ResetPasswordSubject
                };
                await _mailService.SendMailAsync(mailDto);
            }
        }

        public async Task ResetPassword(string password,string token)
        {
            if(string.IsNullOrEmpty(token)) throw new ModelValidationException(MessageConstants.INVALID_TOKEN);
            DateTime dateTime = Convert.ToDateTime(DecodingMailToken(token).Split("&")[1]);
            if (dateTime < DateTime.UtcNow) throw new ModelValidationException(MessageConstants.TOKEN_EXPIRE);

            User user = await _authenticationRepository.GetUserByEmail(DecodingMailToken(token).Split("&")[0]);
            user.Password = password;
            await _authenticationRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<TokensDto> RefreshToken(TokensDto tokenDto)
        {
            ClaimsPrincipal principal =_jwtManageService.GetPrincipalFormExpiredToken(tokenDto.AccessToken);
            string claimtype = ClaimTypes.Email;
            var emailClaim = principal.FindFirst(claimtype);
            var email = emailClaim?.Value?.Replace("mailto:", string.Empty);
            UserRefreshTokens savedRefreshToken = await _authenticationRepository.GetUserRefreshTokens(email.ToString(), tokenDto.RefreshToken);
            TokensDto newJwtToken = _jwtManageService.GenerateRefreshToken(await _authenticationRepository.GetUserByEmail(email.ToString()));
            if (savedRefreshToken.RefreshToken != tokenDto.RefreshToken || newJwtToken == null) throw new ModelValidationException(MessageConstants.INVALID_ATTEMPT);

            UserRefreshTokens userRefreshTokens = new()
            {
                RefreshToken = newJwtToken.RefreshToken,
                Email = email.ToString(),
            };
            await _authenticationRepository.DeleteUserRefreshToken(email.ToString(), tokenDto.RefreshToken);
            await _authenticationRepository.AddUserRefreshToken(userRefreshTokens);
            await _unitOfWork.SaveAsync();
            return newJwtToken;
        }
        #endregion


        #region HelperMethod

        public static string EncodingMailToken(string email) => System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(email + "&" + DateTime.UtcNow.AddMinutes(10)));
        public static string DecodingMailToken(string token) => System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(token));
        #endregion HelperMethod
    }
}
