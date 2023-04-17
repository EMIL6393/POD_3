using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Impl;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Models;

namespace POD_3.Api.Controllers.SubscriptionManagementMod
{
    public class SubscriptionsController : BaseController
    {
        private readonly DefaultContext context;
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        //private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;


        public SubscriptionsController(DefaultContext context, IMapper mapper, IRepositoryWrapper wrapper)
        {
            this.context = context;
            this.mapper = mapper;
            repository = wrapper;
            //_subscriptionPlanRepository = subscriptionPlanRepository;
        }

        [HttpGet("plan")]
        public async Task<IActionResult> GetPlans()
        {
            var result = await repository.SubscriptionPlanRepository.GetAllAsync();
            var userEntity = mapper.Map<IEnumerable<SubscriptionPlanModel>>(result);
            return GenerateSuccessResponse(userEntity);
        }

      [HttpGet("{username}")]
       public async Task<IActionResult> GetSubscription(string username)
       {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            var result = await repository.UserSubscriptionRepository.GetByUsernameAsync(username);
            var userEntity = mapper.Map<SubscriptionDetailModel>(result);
            var plan = await repository.SubscriptionPlanRepository.GetByIdAsync(result.PlanId);
            userEntity.Plan = mapper.Map<SubscriptionPlanModel>(plan);
            return GenerateSuccessResponse(userEntity);
       }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseSubscription(SubscriptionRequestModel subscriptionRequestModel)
        {
            var subscriptionEntity = mapper.Map<UserSubscription>(subscriptionRequestModel);

            subscriptionEntity.SubscriptionStatus = "New";

            var subscription = await repository.UserSubscriptionRepository.GetByUsernameAsync(subscriptionEntity.UserName);
            if (subscription != null)
            {
                return GenerateErrorResponse(null, errorMessage: $"Subscription for {subscriptionEntity.UserName} already exsits");
            }

            subscriptionEntity.PlanId = await repository.SubscriptionPlanRepository.GetByNameAsync(subscriptionRequestModel.PlanName);

            await repository.UserSubscriptionRepository.AddAsync(subscriptionEntity);
            await repository.SaveAsync();

            return GenerateSuccessResponse($"Subscription for {subscriptionEntity.UserName} created");
        }

        [HttpPut("{subscriptionid}/renew")]
        public async Task<IActionResult> RenewSubscription(int subscriptionid, SubscriptionRequestModel subscriptionRequestModel)
        {
            if ( subscriptionid <= 0 )
            {
                throw new ArgumentException(nameof(subscriptionid));
            }
            if ( subscriptionRequestModel == null )
            {
                throw new ArgumentNullException(nameof(subscriptionRequestModel));
            }

            var subscription = await repository.UserSubscriptionRepository.GetByIdAsync(subscriptionid);
            if ( subscription == null )
            {
                return GenerateErrorResponse(null, errorMessage: $"Subscription doesn't exist");
            }

            var subscriptionEntity = mapper.Map<UserSubscription>(subscriptionRequestModel);
            subscriptionEntity.SubscriptionStatus = "Renewed";
            subscriptionEntity.SubscriptionId = subscriptionid;

            await repository.UserSubscriptionRepository.UpdatePlanAsync(subscriptionEntity);
            await repository.SaveAsync();

            return GenerateSuccessResponse($"Subscription for {subscriptionEntity.UserName} renewed");
        }
        
        [HttpPut("{subscriptionid}/cancel")]
        public async Task<IActionResult> CancelSubscription(int subscriptionid, SubscriptionCancelationRequestModel subscriptionCancelationRequest)
        {
            if ( subscriptionid <= 0 )
            {
                throw new ArgumentException(nameof(subscriptionid));
            }
            if (subscriptionCancelationRequest == null )
            {
                throw new ArgumentNullException(nameof(subscriptionCancelationRequest));
            }

            var subscription = await repository.UserSubscriptionRepository.GetByIdAsync(subscriptionid);
            if ( subscription == null )
            {
                return GenerateErrorResponse(null, errorMessage: $"Subscription doesn't exist");
            }
            if (subscription.SubscriptionStatus == "Cancelled")
            {
                return GenerateErrorResponse(null, errorMessage: $"Subscription already canceled");
            }

            var cancelationEntity = mapper.Map<SubscriptionCancellation>(subscriptionCancelationRequest);
            cancelationEntity.SubscriptionId = subscriptionid;
            await repository.SubscriptionCancellationRepository.AddAsync(cancelationEntity);

            subscription.SubscriptionStatus = "Cancelled";
            await repository.UserSubscriptionRepository.UpdatePlanAsync(subscription);

            await repository.SaveAsync();

            return GenerateSuccessResponse($"Subscription for {subscription.UserName} canceled");
        }
    }
}
