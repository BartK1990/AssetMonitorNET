using System.ServiceProcess;

namespace WindowsDataLib
{
    public static class WindowsService
    {
        public static string GetServiceState(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return sc.Status.ToString();
            }
            catch (System.Exception)
            {
                return @"N/A";
            }
        }
    }
}
