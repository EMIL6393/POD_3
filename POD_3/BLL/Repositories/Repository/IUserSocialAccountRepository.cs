﻿using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface IUserSocialAccountRepository
    {
        Task<UserSocialAccount?> GetByUsernameAsync(string username);
        Task<UserSocialAccount?> GetByIdAsync(int id);
        Task AddAsync(UserSocialAccount account);
        Task<bool> Delete(int id);
        Task UpdatePlanAsync (UserSocialAccount updatedSocialAccount);

        Task<int> GetByUsernameAsyncCount(string username);

    }
}
