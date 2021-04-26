using Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.EFCore.EntityTypeConfiguration
{
    public class UserSubscriptionETC : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.HasKey(k => new {k.UserId,k.SubscriptionPeriodId,k.SubscriptionTypeId });

            builder.HasOne(o => o.SubscriptionType)
                .WithMany(m => m.UserSubscriptions)
                .HasForeignKey(f => f.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.User)
              .WithMany(m => m.UserSubscriptions)
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.SubscriptionPeriod)
             .WithMany(m => m.UserSubscriptions)
             .HasForeignKey(f => f.SubscriptionPeriodId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.SubscriptionTypeId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.Timestamp).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();

            builder.HasIndex(i => i.UserId);
            builder.HasIndex(i => i.SubscriptionTypeId);
        }
    }
}
