using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Service.Domain;
using ArcaneStars.Service.Domain.Aggregates;
using ArcaneStars.Service.Domain.Events;
using Gooios.VerificationService.Proxies;

namespace ArcaneStars.Service.Events
{
    [HandlesAsynchronously]
    public class SendSmsHandler : INotificationHandler<VerificationCreatedEvent>
    {
        readonly ISmsProxy _smsProxy;
        public SendSmsHandler()
        {
            _smsProxy = IocProvider.GetService<ISmsProxy>();
        }

        public Task Handle(VerificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            string templateId = "";

            var verification = notification.Source as Verification;
            if (verification.BizCode == BizCode.Register) templateId = "33592";

            if (verification.BizCode == BizCode.ForgetPassword) templateId = "33593";

            if (verification.BizCode == BizCode.Login) templateId = "900677";

            if (!string.IsNullOrEmpty(templateId))
                _smsProxy.SendVerificationCode(verification.Code, verification.To, templateId);

            return Task.CompletedTask;
        }
    }
}
