using AssetMonitorService.Monitor.Model.Email;

namespace AssetMonitorService.Monitor.SingletonServices.Email
{
    public interface IAssetsNotificationDataSharedService : IAssetsSharedServiceBase<AssetNotificationData>
    {
        void SendEmailNotifications();
    }
}
