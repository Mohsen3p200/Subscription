using Domain.Entitties;
using Domain.IRepositories.IRepository;
using Infrastructure.Persistance.EFCore;

namespace Infrastructure.Persistance.Repositories.Repository
{
    public class SubscriptionTypeRepository : GenericRepository<SubscriptionType>, ISubscriptionTypeRepository
    {
        public SubscriptionTypeRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
