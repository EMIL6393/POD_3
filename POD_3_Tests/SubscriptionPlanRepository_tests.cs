using POD_3.BLL.Repositories.Impl;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POD_3_Tests.DataSetup;

namespace POD_3_Tests
{
    [TestFixture]
    internal class SubscriptionPlanRepository_tests
    {
        UserSubscriptionRepository _subscriptionRepository;

        [SetUp] 
        public void SetUp()
        {

            _subscriptionRepository = new UserSubscriptionRepository(TestSetup.Context);


        
        }

        [Test]

        public async Task GetbyId_returns_Subscription()
        {
            var result = await _subscriptionRepository.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.UserName == "JohnDoe" && result.PlanId == 1);

        }

        [Test]

        public async Task GetbyId_returns_null()
        {
            var result = await _subscriptionRepository.GetByIdAsync(10);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetbyName_returns_subscription()
        {
            var result = await _subscriptionRepository.GetByUsernameAsync("jikku");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SubscriptionId == 2 && result.PlanId==2 && result.SubscriptionStatus!="Cancelled");
        }

        public async Task GetbyName_statusError()
        {
            var result = await _subscriptionRepository.GetByUsernameAsync("sam");

            Assert.IsNull(result);
            Assert.IsTrue(result.SubscriptionStatus == "Cancelled");

        }

        [Test]
        public async Task AddAsync_sucess()
        {
            var sub = new UserSubscription()
            {

                SubscriptionId = 4,
                UserName = "emil",
                UserId = 3,
                PlanId = 1,
                SubscriptionStartDate = DateTime.UtcNow.AddDays(-30),
                SubscriptionEndDate = DateTime.UtcNow.AddDays(30),
                AmountPaid = 10.00,
                PaymentMode = "Credit Card",
                SubscriptionStatus = "New"
            };

            await _subscriptionRepository.AddAsync(sub);
            TestSetup.Context.SaveChangesAsync();

            var result = await _subscriptionRepository.GetByUsernameAsync("emil");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SubscriptionId == 4 && result.PlanId == 1 && result.SubscriptionStatus != "Cancelled");
        }

        [Test]
        public async Task UpdateAsync_sucess()
        {
            var sub = new UserSubscription()
            {

                SubscriptionId = 4,
                UserName = "emil",
                UserId = 3,
                PlanId = 2,
                SubscriptionStartDate = DateTime.UtcNow.AddDays(-30),
                SubscriptionEndDate = DateTime.UtcNow.AddDays(30),
                AmountPaid = 25.00,
                PaymentMode = "Credit Card",
                SubscriptionStatus = "Renewed"
            };

            await _subscriptionRepository.UpdatePlanAsync (sub);
            TestSetup.Context.SaveChangesAsync();

            var result = await _subscriptionRepository.GetByUsernameAsync("emil");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.PlanId == 2 && result.SubscriptionStatus == "Renewed");
        }
    }
}
