using System;
using System.Collections.Generic;

namespace Domain.Entitties
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public IList<UserSubscription> UserSubscriptions { get; set; }
    }
}
