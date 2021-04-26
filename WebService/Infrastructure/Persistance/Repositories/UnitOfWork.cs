using Domain.IRepositories;
using Domain.IRepositories.IRepository;
using Infrastructure.Persistance.EFCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _transaction;

        public IUserSubscriptionRepository UserSubscription  { get; private set; }
        public IUserRepository User { get; private set; }
        public ISubscriptionPeriodRepository SubscriptionPeriod { get; private set; }
        public ISubscriptionTypeRepository SubscriptionType { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            UserSubscription = new UserSubscriptionRepository(db);
            User = new UserRepository(db);
            SubscriptionType = new SubscriptionTypeRepository(db);
            SubscriptionPeriod = new SubscriptionPeriodRepository(db);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
