using Application.Handlers.Subscriptions.Models;
using Domain.Entitties;
using MediatR;

namespace Application.Handlers.Subscriptions.Commands
{
    public class CreateSubscriptionForUserCommand:IRequest<UserSubscription>
    {
        public CreateSubscriptionForUserModel CreateSubscription { get; set; }
    }
}
