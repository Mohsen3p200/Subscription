using Application.Handlers.Subscriptions.Models;
using Application.Handlers.Subscriptions.Models.Dtos;
using Domain.Entitties;
using MediatR;
using Refit;
using System.Threading.Tasks;

namespace ClientLibrary.SDK
{
    public interface ISubscriptions
    {
        [Get("/api/Subscriptions")]
        Task<ApiResponse<UserSubscription>> CreateSubscription([Body] CreateSubscriptionForUserModel model);

        [Delete("/api/Subscriptions/{id}")]
        Task<ApiResponse<Unit>> DeleteSubscription(int id);

        [Get("/api/Subscriptions/{id}")]
        Task<ApiResponse<UserSubscriptionDto>> GetSubscription(int id);
    }
}
