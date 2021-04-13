using System;
using System.Diagnostics;
using MailService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MailService.Implementations
{
    public class CloudMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public CloudMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void SendMail(string subject, string message)
        {
            Debug.WriteLine(string.Format("Mail from {0} to {1}, with CloudMailService.", _configuration["mailSettings:mailFromAddress"], _configuration["mailSettings:mailToAddress"]));
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
