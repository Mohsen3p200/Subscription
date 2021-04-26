using Domain.Entitties;
using Domain.IRepositories.IRepository;
using Infrastructure.Persistance.EFCore;

namespace Infrastructure.Persistance.Repositories.Repository
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext db)
            :base(db)
        {

        }
    }
}
