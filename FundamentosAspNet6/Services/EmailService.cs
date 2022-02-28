using System.Net.Mail;
using System.Net;

namespace FundamentosAspNet6.Services;

public class EmailService
{
    private readonly string EMAIL;
    private readonly string SENHA;

    public EmailService(IConfiguration configuration)
    {
        EMAIL = configuration.GetSection("EmailConfig").GetValue<string>("email");
        SENHA = configuration.GetSection("EmailConfig").GetValue<string>("senha");
    }
    public async Task<bool> SendEmail(string subject, string body, string destination)
    {
        var mailMessage = new MailMessage(new MailAddress(EMAIL, "displayName"), new MailAddress(destination, "displayName"))
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(EMAIL, SENHA);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.Timeout = (int)TimeSpan.FromSeconds(20).TotalMilliseconds;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}