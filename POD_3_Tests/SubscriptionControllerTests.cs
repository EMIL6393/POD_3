using AutoMapper;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POD_3.Api.Controllers.SubscriptionManagementMod;
using POD_3.BLL.Repositories.Impl;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Models;
using static POD_3_Tests.DataSetup;

namespace POD_3_Tests
{
    [TestFixture]
    public class SubscriptionControllerTests
    {

        private Mock<IConfiguration> mockConfig;
        private Mock<IMapper> mockMapper;
        private SubscriptionsController subscriptionsController;

        [SetUp]
        public void Setup()
        {
            //var testSetup = new TestSetup();
            mockConfig = new Mock<IConfiguration>();
            mockMapper = new Mock<IMapper>();
            subscriptionsController = new SubscriptionsController(
                mockMapper.Object,
                new RepositoryWrapper(TestSetup.Context)
                );       
        }

        [Test]
        public async Task GetPlans_returnsSubscriptionPlans()
        {
            var response = await subscriptionsController.GetPlans() as ObjectResult;
            var result = response.Value as DefaultResponseModel<List<SubscriptionPlanModel>>;
            var plans = result.Data;

            var subscriptionPlans = new List<SubscriptionPlan>
                {
                    new SubscriptionPlan() { PlanId = 1, Name = "basic", PricePerMonth = 10 },
                    new SubscriptionPlan() { PlanId = 2, Name = "pro", PricePerMonth = 25 }
                };

            Assert.IsTrue(plans[0].Name =="basic" && plans[0].PricePerMonth ==10 );

        }
    }
}