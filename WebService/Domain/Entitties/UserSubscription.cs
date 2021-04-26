using System;

namespace Domain.Entitties
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public int SubscriptionTypeId { get; set; }
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
