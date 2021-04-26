using Application.Handlers.Subscriptions.Models.Dtos;
using MediatR;

namespace Application.Handlers.Subscriptions.Queries
{
    public class GetSubscriptionByIdQuery:IRequest<UserSubscriptionDto>
    {
        public int SubscriptionId { get; set; }
        public GetSubscriptionByIdQuery(int subscriptionId)
        {
            SubscriptionId = subscriptionId;
        }   
    }
}
