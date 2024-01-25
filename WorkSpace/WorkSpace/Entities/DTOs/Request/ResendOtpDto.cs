using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Request
{
    public class ResendOtpDto : BaseValidationModel<ResendOtpDto>
    {
        public string Email { get; set; } = String.Empty;
    }
}
