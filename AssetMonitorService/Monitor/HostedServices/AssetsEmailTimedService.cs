using AssetMonitorService.Monitor.Model.Email;
using AssetMonitorService.Monitor.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AssetMonitorService.Monitor.HostedServices
{
    public class AssetsEmailTimedService : AssetsTimedServiceBase<AssetsEmailTimedService>
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        protected readonly object taskTimedJobLock = new object();

        private int emailTest = 1;

        public AssetsEmailTimedService(IServiceScopeFactory scopeFactory,
            ILogger<AssetsEmailTimedService> logger, TimeSpan? scanTime = null) : base(logger: logger, scanTime: scanTime)
        {
            this._scopeFactory = scopeFactory;
        }

        protected override void TimedJob()
        {
            // Start all tasks for getting and saving the data
            lock (taskTimedJobLock)
            {
                GetAssetsData();
            }
        }

        private void GetAssetsData()
        {
            //_assetsAlarmDataShared.UpdateAssetsAlarmValues();
            if(emailTest == 1)
            {
                // Email test
                using var scope = _scopeFactory.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var emailMessage = new EmailMessage()
                {
                    FromAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Bartosz", Address = "bk@bartoszku.com" } },
                    ToAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Bartosz K", Address = "kuriata.bartosz@gmail.com" } },
                    Subject = "Test",
                    Content = "Just test"
                };

                emailService.Send(emailMessage);
            }
            emailTest = 2;
        }
    }
}
