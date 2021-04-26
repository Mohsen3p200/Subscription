namespace Application.Handlers.Subscriptions.Models
{
    public class CreateSubscriptionForUserModel
    {
        public int UserId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int SubscriptionPeriodId { get; set; }
    }
}
