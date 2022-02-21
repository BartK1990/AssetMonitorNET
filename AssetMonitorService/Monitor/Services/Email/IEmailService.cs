using AssetMonitorService.Monitor.Model.Email;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.Services.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
