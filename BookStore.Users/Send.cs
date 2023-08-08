using System.Net.Mail;

namespace BookStore.Users
{
    public class Send
    {
        public string SendEmail(string emailTo,string token)
        {
            string emailFrom = "rushikeshkoshti5@gmail.com";
            string subject = "Token genrated ";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com" , 587); // gmail smtp
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("rushikeshkoshti5@gmail.com", "ebwwdgsrlnokenhl");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;

            smtpClient.Send(emailFrom, emailTo, subject, token);

            return emailTo;
        }
    }
}
