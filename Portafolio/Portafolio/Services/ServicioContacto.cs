using Portafolio.Models;
using System.Net;
using System.Net.Mail;

namespace Portafolio.Services {
    public interface IServicioCorreo {
        string EnviarMensaje(ContactoViewModel contactoViewModel);
    }

    public class ServicioContacto : IServicioCorreo {
        public string EnviarMensaje(ContactoViewModel contactoViewModel) {
            try {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(contactoViewModel.Email);
                mail.To.Add("cesarvillalobosolmos.01@gmail.com");
                mail.Subject = "Contacto a traves del portafolio ASP.NET";
                mail.Body = contactoViewModel.Mensaje +
                    $"\n\nAtt: {contactoViewModel.Nombre}";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("cesarvillalobosolmos.01@gmail.com", "xxx");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return "Exito papu";
            } catch (Exception ex) {
                return "Error :(" + ex.Message;
            }
        }
    }
}