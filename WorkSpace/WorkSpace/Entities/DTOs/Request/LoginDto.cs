using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Request
{
    public class LoginDto : BaseValidationModel<LoginDto>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } = false;
    }
}
