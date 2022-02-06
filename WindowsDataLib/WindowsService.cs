using System.ServiceProcess;

namespace WindowsDataLib
{
    public static class WindowsService
    {
        public static int GetServiceState(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return (int)sc.Status;
            }
            catch (System.Exception)
            {
                return -1;
            }
        }
    }
}
