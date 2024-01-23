using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Request
{
    public class LoginOtpDto : BaseValidationModel<LoginOtpDto>
    {
        public string Email { get; set; } = null!;

        public string Otp { get; set; } = null!;
    }

    public class TokensDto : BaseValidationModel<TokensDto>
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
