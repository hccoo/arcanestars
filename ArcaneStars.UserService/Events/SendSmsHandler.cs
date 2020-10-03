using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ArcaneStars.Infrastructure.Events;
using ArcaneStars.Service.Domain;
using ArcaneStars.Service.Domain.Aggregates;
using ArcaneStars.Service.Domain.Events;

namespace ArcaneStars.Service.Events
{
    [HandlesAsynchronously]
    public class SendSmsHandler : INotificationHandler<VerificationCreatedEvent> //IEventHandler<VerificationCreatedEvent>
    {
        public SendSmsHandler()
        {
        }

        public Task Handle(VerificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
