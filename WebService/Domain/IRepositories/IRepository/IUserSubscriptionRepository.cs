using Domain.Entitties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IRepositories.IRepository
{
    public interface IUserSubscriptionRepository:IGenericRepository<UserSubscription>
    {
        Task<UserSubscription> GetSubscriptionWithDetails(int subscriptionId);
        Task<List<UserSubscription>> GetUserSubscriptions(int userId);
        Task<UserSubscription> CheckExistingSubscription(
            int subscriptionTypeId,
            int subscriptionPeriodId,
            int userId
            );
    }
}
