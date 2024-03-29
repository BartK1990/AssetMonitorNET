﻿using AssetMonitorDataAccess.Models.Enums;
using AssetMonitorService.Data.Repositories;
using AssetMonitorService.Monitor.Model.Email;
using AssetMonitorService.Monitor.Services.Email;
using AssetMonitorService.Monitor.SingletonServices.Alarm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetMonitorService.Monitor.SingletonServices.Email
{
    public class AssetsNotificationDataSharedService : AssetsSharedServiceBase<AssetsNotificationDataSharedService, AssetNotificationData>,
        IAssetsNotificationDataSharedService
    {
        private readonly IAssetsAlarmDataSharedService _assetsAlarmDataShared;
        private readonly IEmailConfiguration _emailConfiguration;

        public AssetsNotificationDataSharedService(
            IAssetsAlarmDataSharedService assetsAlarmDataShared,
            IEmailConfiguration emailConfiguration,
            IServiceScopeFactory scopeFactory,
            ILogger<AssetsNotificationDataSharedService> logger) : base(scopeFactory: scopeFactory, logger: logger)
        {
            this._assetsAlarmDataShared = assetsAlarmDataShared;
            this._emailConfiguration = emailConfiguration;
        }

        protected override async Task UpdateAssetsList(IAssetMonitorRepository repository)
        {
            var assets = (await repository.GetAllAssetsAsync()).ToList();
            foreach (var asset in assets)
            {
                // Get Asset properties
                var assetWithProperties = await repository.GetAssetPropertiesByIdAsync(asset.Id);
                var assetProperties = assetWithProperties.AssetPropertyValues;

                // Properties
                bool enabled = GetAssetProperty(asset, assetProperties, AssetPropertyNameEnum.EmailNotificationsEnable, bool.Parse, false);

                // Get Alarm tags
                var alarmShared = _assetsAlarmDataShared.AssetsData.Where(a => a.Id == asset.Id).FirstOrDefault();
                var tags = alarmShared.Data.Keys;

                // Get recipients
                var userEmailAddresses = (await repository.GetUserEmailAddressByAssetIdAsync(asset.Id));

                if (tags != null && tags.Any() && userEmailAddresses != null && userEmailAddresses.Any())
                {
                    var recipients = new List<EmailAddress>();
                    recipients.AddRange(userEmailAddresses.Select(s => new EmailAddress() { Name = s.Name, Address = s.Address }));
                    AssetsData.Add(new AssetNotificationData(tags, recipients, asset.Id, asset.Name, enabled));
                }
            }
        }

        public void SendEmailNotifications()
        {
            using var scope = _scopeFactory.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            foreach (var asset in this.AssetsData)
            {
                var alarms = asset.GetAlarmMessage();
                if(alarms != null)
                {
                    _logger.LogInformation($"{this.GetType().Name} - Sending email notification for asset Id: [{asset.Id}]");

                    var alarmMessage = string.Join(" <br /> ", alarms);
                    var recipients = asset.Recipients.ToList();

                    var emailMessage = new EmailMessage()
                    {
                        FromAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Asset Monitor NET", Address = _emailConfiguration.SmtpUsername } },
                        ToAddresses = recipients,
                        Subject = $"Asset Monitor Alarms for asset [{asset.Name}]",
                        Content = alarmMessage
                    };
                    emailService.Send(emailMessage);
                }
            }
        }

    }

}
