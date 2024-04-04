using Newtonsoft.Json;
using Rabbit.Models.Entities;
using System.Net;
using System.Net.Mail;

namespace Rabbit.Services.Implementations
{
    public class SendMail : ISendMail
    {
        public bool SendNotification(RabbitMessage rabbitMessage)
        {
            // My mail settings
            var senderMail = Environment.GetEnvironmentVariable("mail");
            var senderpassword = Environment.GetEnvironmentVariable("password");

            // Server SMTP Gmail configurations
            var smtpHost = "smtp.gmail.com";
            var smtpPort = 587;

            var message = new MailMessage(senderMail, rabbitMessage.Receiver)
            {
                Subject = rabbitMessage.Title,
                Body = rabbitMessage.Description
            };

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderMail, senderpassword),
                EnableSsl = true
            };

            // Try send mail
            try
            {
                smtpClient.Send(message);
                Console.WriteLine($"E-mail enviado com a mensagem: {JsonConvert.SerializeObject(message)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu algum erro ao enviar o e-mail {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
