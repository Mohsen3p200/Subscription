using MediatR;

namespace Application.Handlers.Subscriptions.Commands
{
    public class DeleteSubscriptionCommand:IRequest
    {
        public DeleteSubscriptionCommand(int subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }

        public int SubscriptionId { get; set; }
    }
}
