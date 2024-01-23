using BusinessAccessLayer.Abstraction;
using Common.Constants;
using Entities.DTOs.Request;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Implementation
{
    public class MailService : IMailService
    {
        #region Properties
        private MailSettingDto _mailSettingDto;
        #endregion

        #region Cunstructor
        public MailService(IOptions<MailSettingDto> mailSetting)
        {
            _mailSettingDto = mailSetting.Value;
        }

        #endregion


        #region Interface Methods

        public async Task SendMailAsync(MailDto mailData, CancellationToken cancellationToken = default)
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSettingDto.Mail);
            email.To.Add(MailboxAddress.Parse(mailData.ToEmail));
            email.Subject = String.IsNullOrEmpty(mailData.Subject) ? mailData.Subject : MailConstants.GenericSubject;

            var builder = new BodyBuilder();

            if(mailData.Attachments != null && mailData.Attachments.Count != 0)
            {
                byte[] fileBytes;
                foreach(var file in mailData.Attachments)
                {
                    if(file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailData.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettingDto.Host,_mailSettingDto.Port,SecureSocketOptions.StartTls,cancellationToken);
            smtp.Authenticate(_mailSettingDto.Mail, _mailSettingDto.Password,cancellationToken);
            await smtp.SendAsync(email,cancellationToken);
            smtp.Disconnect(true,cancellationToken);    
        }

        #endregion
    }
}
