using Hangfire;
using System;

namespace AspMVC_Monitor.Services
{
    public class ServiceProviderActivator : JobActivator
    {
        private IServiceProvider _serviceProvider;

        public ServiceProviderActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetService(jobType);
        }
    }
}
