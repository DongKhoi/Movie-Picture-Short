
namespace Application.InternalServices
{
    public static class EmailService
    {
        public static Task SendAsync(string mailAddress, string subject, string body)
        {
            try
            {
                var client = new System.Net.Mail.SmtpClient("smtp.example.com", 111);//temp
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Port = 587;
                client.Host = "smtp.gmail.com";

                client.Credentials = new System.Net.NetworkCredential("cauviewchome3@gmail.com", "cqxouerrcxzbnxdv");

                var mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress("cauviewchome3@gmail.com");

                mailMessage.To.Add(mailAddress);

                if (!string.IsNullOrEmpty(mailAddress))
                {
                    mailMessage.CC.Add(mailAddress);
                }

                //mailMessage.Subject = "Confirm Email Social Network";
                mailMessage.Subject = "Confirm Email Movie-Web";
                mailMessage.Body = body;

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

                return client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
