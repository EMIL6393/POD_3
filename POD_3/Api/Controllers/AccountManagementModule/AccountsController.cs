using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POD_3.BLL.Repositories.Impl;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.Core;
using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using POD_3.DAL.Models;

namespace POD_3.Api.Controllers.AccountManagementModule
{
    public class AccountsController : BaseController
    {
        private readonly DefaultContext context;
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;

        public AccountsController(DefaultContext context, IMapper mapper, IRepositoryWrapper wrapper)
        {
            this.context = context;
            this.mapper = mapper;
            repository = wrapper;
        }

        [HttpGet("types")]

        public async Task<IActionResult> GetTypes()
        {
            var types = await repository.SocialAccountTypesRepository.GetAllAsync();
            var typeEntity = mapper.Map<IEnumerable<AccountTypesModel>>(types);
            return GenerateSuccessResponse(typeEntity);
        }

       [HttpPost("addsocialaccount")]
        public async Task<IActionResult> AddSocialAccount(AccountRequestModel accountRequestModel)
        {
            if (accountRequestModel == null)
            {
                throw new ArgumentNullException(nameof(accountRequestModel));

            }
            var accountEntity = mapper.Map<UserSocialAccount>(accountRequestModel);
            accountEntity.EncryptedPassword = Util.PasswordHashing(accountRequestModel.Password);
            var subs =  await repository.UserSubscriptionRepository.GetByUsernameAsync(accountRequestModel.UserName);
            if(subs == null)
            {
                GenerateErrorResponse(null, errorMessage: "Subscription dosn't exist");
            }
            var plan = await repository.SubscriptionPlanRepository.GetByIdAsync(subs.PlanId);
            accountEntity.SubscriptionName = plan.Name;
            accountEntity.SocialAccountTypeId = await repository.SocialAccountTypesRepository.GetByNameAsync(accountRequestModel.SocialAccount);

            await repository.UserSocialAccountRepository.AddAsync(accountEntity);

            await repository.SaveAsync();

            return GenerateSuccessResponse(accountEntity);

        }
       




    }
}
