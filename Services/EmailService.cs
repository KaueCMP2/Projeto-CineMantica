using System.Net;
using System.Net.Mail;

namespace EsqueceuSenha.Services
{
    public class EmailService
    {
        public int CodigoGerado { get; private set; }
        public readonly string _emailDestino;

        public EmailService(string emailDestino)
        {
            _emailDestino = emailDestino;
            CodigoGerado = new Random().Next(100000, 999999);
        }

        public async Task EnviarCodigo()
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("cinemantica3@gmail.com", "qrhzjbpfdmqqrqph")
            };
            
            var mail = new MailMessage("cinemantica3@gmail.com", _emailDestino)
            {
                Subject = "Código para alterar senha",
                Body = $"Seu código para redefinir a senha é: {CodigoGerado}",
                IsBodyHtml = false
            };
            await smtp.SendMailAsync(mail);
        }
    }
}