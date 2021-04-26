using Application.Handlers.Subscriptions.Models.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.SDK
{
    public interface IUsersAPI
    {
        [Get("/api/Users/{id}")]
        Task<ApiResponse<List<UserSubscriptionDto>>> GetSubscription(int id);
    }
}
