using Application.Handlers.Subscriptions.Models.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Application.Handlers.Users.Queries
{
    public class GetAllUserSubscriptionQuery:IRequest<List<UserSubscriptionDto>>
    {  
        public int UserId { get; set; }
        public GetAllUserSubscriptionQuery(int userId)
        {
            UserId = userId;
        }
    }
}
