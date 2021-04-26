using Domain.Entitties;
using Domain.IRepositories.IRepository;
using Infrastructure.Persistance.EFCore;

namespace Infrastructure.Persistance.Repositories.Repository
{
    public class SubscriptionPeriodRepository:GenericRepository<SubscriptionPeriod>, ISubscriptionPeriodRepository
    {
        public SubscriptionPeriodRepository(ApplicationDbContext db)
          :base(db)
        {

        }
    }
}
