using Domain.Entitties;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.EFCore.Seeds
{
    public class Seed:IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly ApplicationDbContext context;

        public Seed(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SeedData()
        {
            await BeginTransactionAsync();

            await CreateUsers();

            await CreateSubscriptionTypes();

            await CreateSubscriptionPeriods();

            await context.SaveChangesAsync();

            await CommitAsync();
        }

        private async Task CreateUsers()
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
               {
                new User
                {
                    Firstname="User1",
                    Lastname="User1 Lastname",
                    Address="Denamrk",
                    Email="User1@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
                new User
                {
                    Firstname="User2",
                    Lastname="User2 Lastname",
                    Address="Denamrk",
                    Email="User2@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
                new User
                {
                    Firstname="User3",
                    Lastname="User3 Lastname",
                    Address="Denamrk",
                    Email="User3@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
               new User
                {
                    Firstname="User4",
                    Lastname="User4 Lastname",
                    Address="Denamrk",
                    Email="User4@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
                new User
                {
                    Firstname="User5",
                    Lastname="User5 Lastname",
                    Address="Denamrk",
                    Email="User5@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
                 new User
                {
                    Firstname="User6",
                    Lastname="User6 Lastname",
                    Address="Denamrk",
                    Email="User6@gamil.com",
                    PhoneNumber="000111",
                    IsActive=true,
                    Timestamp=DateTime.UtcNow
                },
            };

                await context.Users.AddRangeAsync(users);
            }
        }

        private async Task CreateSubscriptionTypes()
        {
            if (!context.SubscriptionTypes.Any())
            {
                var subscriptions = new List<SubscriptionType>()
                {
                    new SubscriptionType()
                    {
                        Name="TV",
                        Description="TV Subscription",
                        Price=2,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                    new SubscriptionType()
                    {
                        Name="Broadband",
                        Description="Broadband Subscription",
                        Price=5,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                    new SubscriptionType()
                    {
                        Name="Phone",
                        Description="Phone Subscription",
                        Price=3,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                    new SubscriptionType()
                    {
                        Name="Music Store",
                        Description="Music Store Subscription",
                        Price=8,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                };

                await context.SubscriptionTypes.AddRangeAsync(subscriptions);
            }
        }

        private async Task CreateSubscriptionPeriods()
        {
            if (!context.SubscriptionTypes.Any())
            {
                var subscriptionPeriods = new List<SubscriptionPeriod>()
                {
                    new SubscriptionPeriod()
                    {
                        Name="Daily",
                        Description="Daily Subscription",
                        Price=2,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                     new SubscriptionPeriod()
                    {
                        Name="Weekly",
                        Description="Weekly Subscription",
                        Price=5,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                    new SubscriptionPeriod()
                    {
                        Name="Monthly",
                        Description="Monthly Subscription",
                        Price=15,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                    new SubscriptionPeriod()
                    {
                        Name="Yearly",
                        Description="Yearly Subscription",
                        Price=115,
                        IsActive=true,
                        Timestamp=DateTime.UtcNow
                    },
                };

                await context.SubscriptionPeriods.AddRangeAsync(subscriptionPeriods);
            }
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
        }
        public void Dispose()
        {
           context.Dispose();
        }
    }
}
