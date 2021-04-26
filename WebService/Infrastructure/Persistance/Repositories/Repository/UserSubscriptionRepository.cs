using Domain.Entitties;
using Domain.IRepositories.IRepository;
using Infrastructure.Persistance.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Repository
{
    public class UserSubscriptionRepository
        :GenericRepository<UserSubscription>,IUserSubscriptionRepository
    {
        private readonly ApplicationDbContext _db;

        public UserSubscriptionRepository(ApplicationDbContext db)
            :base(db)
        {
            _db = db;
        }

        public async Task<UserSubscription> CheckExistingSubscription
            (int subscriptionTypeId, int subscriptionPeriodId, int userId)
        {
            var subscription= await  _db.UserSubscriptions
                 .Where(x => x.SubscriptionTypeId == subscriptionTypeId &&
                 x.SubscriptionPeriodId == subscriptionPeriodId &&
                 x.UserId == userId
                 )
                .SingleOrDefaultAsync();

            return subscription;
        }

        public Task<UserSubscription> GetSubscriptionWithDetails(int subscriptionId)
        {
            if (subscriptionId == 0)
                throw new ArgumentNullException(nameof(subscriptionId));

            var subscription = _db.UserSubscriptions
                 .Include(x => x.User)
                 .Include(x => x.SubscriptionType)
                 .Include(x => x.SubscriptionPeriod)
                 .SingleOrDefaultAsync(x => x.Id == subscriptionId);

            return subscription;
        }

        public Task<List<UserSubscription>> GetUserSubscriptions(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));

            var subscriptions = _db.UserSubscriptions
                 .Include(x => x.User)
                 .Include(x => x.SubscriptionType)
                 .Include(x => x.SubscriptionPeriod)
                 .Where(x => x.UserId == userId)
                 .ToListAsync();

            return subscriptions;
        }
    }
}
