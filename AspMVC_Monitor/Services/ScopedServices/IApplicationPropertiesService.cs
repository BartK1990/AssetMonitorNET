using AssetMonitorDataAccess.Models.Enums;
using System;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Services.ScopedServices
{
    public interface IApplicationPropertiesService
    {
        string FuncString(string s);
        Task<TParam> GetProperty<TParam>(ApplicationPropertyNameEnum appPropertyName, Func<string, TParam> parse, TParam defaultValue);
    }
}
