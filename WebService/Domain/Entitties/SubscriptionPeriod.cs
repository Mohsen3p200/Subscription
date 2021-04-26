using System;
using System.Collections.Generic;

namespace Domain.Entitties
{
    public class SubscriptionPeriod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public IList<UserSubscription> UserSubscriptions { get; set; }
    }
}
