using BEApi.Dtos;

namespace BEApi.Servies
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}