//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace LoginPageAPIs.Services
//{
//    public class EmailService
//    {
//        private readonly IConfiguration _configuration;

//        public EmailService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task<bool> SendEmailReportAsync(string email, string subject, string body)
//        {
//            try
//            {
//                var fromAddress = new MailAddress("your-email@gmail.com", "Your Name");
//                var toAddress = new MailAddress(email);
//                var smtp = new SmtpClient
//                {
//                    Host = _configuration["EmailSettings:SmtpHost"],
//                    Port = int.Parse(_configuration["EmailSettings:SmtpPort"]),
//                    EnableSsl = true,
//                    Credentials = new NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"])
//                };


//                var message = new MailMessage(fromAddress, toAddress)
//                {
//                    Subject = subject,
//                    Body = body
//                };

//                await smtp.SendMailAsync(message);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}

using LoginPageAPIs.Models;
using LoginPageAPIs.Models.SmtpSettings;
using Microsoft.Extensions.Options;
using MimeKit;

public class EmailService
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<EmailService> _logger;
    private readonly AppDbContext _context;

    public EmailService(IOptions<SmtpSettings> opts, ILogger<EmailService> logger , AppDbContext context)
    {
        _settings = opts.Value;
        _logger = logger;
        _context = context;
    }

    public async Task<bool> SendChildReportAsync(int childId, string subject, string body)
    {
        var child = await _context.Users.FindAsync(childId);
        if (child == null || string.IsNullOrEmpty(child.Email))
        {
            _logger.LogWarning("Child not found or email is missing.");
            return false;
        }

        return await SendEmailReportAsync(child.Email, subject, body);
    }

    public async Task<bool> SendEmailReportAsync(string to, string subject, string body)
    {
        try
        {
            if (!MailboxAddress.TryParse(to, out _))
            {
                _logger.LogWarning("Invalid email format: {Email}", to);
                return false;
            }

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_settings.FromName, _settings.From));
            msg.To.Add(MailboxAddress.Parse(to.Trim()));
            msg.Subject = subject;
            msg.Body = new TextPart("plain") { Text = body };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            // 🔥 حل المشكلة: تجاهل مشاكل الشهادة (مؤقتًا)
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to send email to {Email}. Exception: {Message}", to, ex.Message);
            return false;
        }
    }


}

