using AssetMonitorDataAccess.Models.Enums;
using System;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.Services
{
    public interface IApplicationPropertiesService
    {
        string FuncString(string s);
        Task<TParam> GetProperty<TParam>(ApplicationPropertyNameEnum appPropertyName, Func<string, TParam> parse, TParam defaultValue);
    }
}