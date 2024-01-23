using Entities.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Abstraction
{
    public interface IMailService
    {
        Task SendMailAsync(MailDto mailData, CancellationToken cancellationToken = default);
    }
}
