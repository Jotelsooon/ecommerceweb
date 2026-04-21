using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ECommerceWeb.Utility;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config) => _config = config;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var settings = _config.GetSection("EmailSettings");

        using var client = new SmtpClient(settings["Host"],
            int.Parse(settings["Port"]!))
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(
                settings["Username"], settings["Password"])
        };

        var msg = new MailMessage
        {
            From = new MailAddress(settings["FromEmail"]!, settings["FromName"]),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        msg.To.Add(email);
        await client.SendMailAsync(msg);
    }
}