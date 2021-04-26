using Application.Handlers.Subscriptions.Models;
using Application.Handlers.Subscriptions.Models.Dtos;
using ClientLibrary.SDK;
using Domain.Entitties;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLibrary
{
    class Program
    {
        static string baseUrl = "https://localhost:44324";

        private static ISubscriptions _subscriptionApi = 
            RestService.For<ISubscriptions>(baseUrl);

        private static IUsersAPI usersApi = 
            RestService.For<IUsersAPI>(baseUrl);

        static async Task Main(string[] args)
        {
            //await GetSubscriptionsForUser();

            //await DeleteSubscription(1);

            //await Getsubscription(1);

            //await CreateSubscription(1,1,1);

        }

        private static async Task<UserSubscription> CreateSubscription(
            int subscriptionPeriodId,
            int subscriptionTypeId,
            int userId
            )
        {
            var subscription = new CreateSubscriptionForUserModel()
            {
                SubscriptionPeriodId = subscriptionPeriodId,
                SubscriptionTypeId = subscriptionTypeId,
                UserId = userId
            };

            var result = await _subscriptionApi.CreateSubscription(subscription);

            return result.Content;
        }

        private static async Task<UserSubscriptionDto> Getsubscription(int id)
        {
            var subscription= await _subscriptionApi.GetSubscription(id);

            return subscription.Content;
        }

        private static async Task DeleteSubscription(int id)
        {
            await _subscriptionApi.DeleteSubscription(id);
        }

        private static async Task GetSubscriptionsForUser()
        {
            var userSubscriptions = await usersApi.GetSubscription(1);

            var json = JsonConvert.SerializeObject(userSubscriptions.Content, Formatting.Indented,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling =ReferenceLoopHandling.Ignore
            });

            List<UserSubscriptionDto> dto = JsonConvert.DeserializeObject<List<UserSubscriptionDto>>(json);

            foreach (var item in dto)
            {
                Console.WriteLine(item.Timestamp);
            }
        }
    }
}
