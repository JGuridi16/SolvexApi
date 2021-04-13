namespace MailService.Interfaces
{
    public interface IMailService
    {
        void SendMail(string subject, string message);
    }
}
