using Domain.IRepositories.IRepository;
using System;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IUserSubscriptionRepository UserSubscription { get; }
        IUserRepository User { get; }
        ISubscriptionPeriodRepository SubscriptionPeriod { get; }
        ISubscriptionTypeRepository SubscriptionType { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
