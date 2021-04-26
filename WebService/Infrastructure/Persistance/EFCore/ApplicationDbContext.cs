using Domain.Entitties;
using Infrastructure.Persistance.EFCore.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.EFCore
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<SubscriptionPeriod> SubscriptionPeriods { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserETC());
            modelBuilder.ApplyConfiguration(new SubscriptionTypeETC());
            modelBuilder.ApplyConfiguration(new SubscriptionPeriodETC());
            modelBuilder.ApplyConfiguration(new UserSubscriptionETC());
        }
    }
}
