using AssetMonitorService.Monitor.Model.Alarm;

namespace AssetMonitorService.Monitor.SingletonServices.Alarm
{
    public interface IAssetsAlarmDataSharedService : IAssetsSharedServiceBase<AssetAlarmData>
    {
        void UpdateAssetsAlarmValues();
    }
}
