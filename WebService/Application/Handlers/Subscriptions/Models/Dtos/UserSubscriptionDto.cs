using System;

namespace Application.Handlers.Subscriptions.Models.Dtos
{
    public class UserSubscriptionDto
    {
        public int Id { get; set; }
        public int SubscriptionTypeId { get; set; }
        public string SubscriptionType { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public string SubscriptionPeriod { get; set; }
        public int UserId { get; set; }
        public string NameOfUser { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
