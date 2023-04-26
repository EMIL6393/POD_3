using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POD_3.Context;
using POD_3.DAL.Entity;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD_3_Tests
{

    public class MoqDbContext : DefaultContext
    {
        public MoqDbContext(DbContextOptions<MoqDbContext> options)
        {
            Options = options;
        }
        public DbContextOptions<MoqDbContext> Options { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "test_Database");
        }
    }

    internal class DataSetup
    {

        [SetUpFixture]
        public class TestSetup
        {
            public static MoqDbContext Context { get; private set; }
            public static string JWT_KEY => "aHMmt8rzjJ6TigNiZw3I";

            [OneTimeSetUp]
            public void TestSetupInit()
            {
                var options = new DbContextOptionsBuilder<MoqDbContext>()
                    .UseInMemoryDatabase(databaseName: "test_database")
                    .Options;

                Context = new MoqDbContext(options);

                var subscriptionPlans = new List<SubscriptionPlan>
                {
                    new SubscriptionPlan() { PlanId = 1, Name = "basic", PricePerMonth = 10 },
                    new SubscriptionPlan() { PlanId = 2, Name = "pro", PricePerMonth = 25 }
                };

                Context.SubscriptionPlans.AddRange(subscriptionPlans);
                Context.SaveChanges();

                var userSubscriptions = new List<UserSubscription>
                {
                    new UserSubscription()
                    {

                        SubscriptionId = 1,
                        UserName = "JohnDoe",
                        UserId = 1,
                        PlanId = 1,
                        SubscriptionStartDate = DateTime.UtcNow.AddDays(-30),
                        SubscriptionEndDate = DateTime.UtcNow.AddDays(30),
                        AmountPaid = 10.00,
                        PaymentMode = "Credit Card",
                        SubscriptionStatus = "New"

                    },
                    new UserSubscription()
                    {

                        SubscriptionId = 2,
                        UserName = "jikku",
                        UserId = 2,
                        PlanId = 2,
                        SubscriptionStartDate = DateTime.UtcNow.AddDays(-30),
                        SubscriptionEndDate = DateTime.UtcNow.AddDays(30),
                        AmountPaid = 25.00,
                        PaymentMode = "Credit Card",
                        SubscriptionStatus = "Renewed"
                    },
                    new UserSubscription()
                    {

                        SubscriptionId = 3,
                        UserName = "sam",
                        UserId = 3,
                        PlanId = 2,
                        SubscriptionStartDate = DateTime.UtcNow.AddDays(-30),
                        SubscriptionEndDate = DateTime.UtcNow.AddDays(30),
                        AmountPaid = 25.00,
                        PaymentMode = "Credit Card",
                        SubscriptionStatus = "Cancelled"
                    }
                };

                Context.UserSubscriptions.AddRange(userSubscriptions);
                Context.SaveChanges();
            }

                [OneTimeTearDown]

                public void ClearDB()
                {
                    Context.Database.EnsureDeleted();
                    Context.Dispose();
                }

            
        }
    }
}