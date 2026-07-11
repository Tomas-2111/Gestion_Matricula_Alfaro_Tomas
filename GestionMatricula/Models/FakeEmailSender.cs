using Microsoft.AspNetCore.Identity.UI.Services;

namespace Semana9.Models
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Simula
            System.Diagnostics.Debug.WriteLine($"Email to: {email}, Subject: {subject}, Message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
