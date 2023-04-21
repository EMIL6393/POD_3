using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using POD_3.BLL.Repositories.Repository;
using POD_3.Core;
using POD_3.DAL.Models;

namespace POD_3.Api.Controllers.SubscriptionManagementMod
{
    public class LoginController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IRepositoryWrapper repository;
        private readonly IConfiguration configuration;
        public LoginController(IMapper mapper, IRepositoryWrapper wrapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            repository = wrapper;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var hash = Util.PasswordHashing(loginRequestModel.Password);
            var user = await repository.LoginRepository.UserLogin(loginRequestModel.Email, hash);
            if (user == null)
                return Unauthorized("Invalid Username or Password");

            var role = user.Role;
            var token = JWT.GenerateToken(new Dictionary<string, string>
            {
                { "Role", user.Role },
                {JwtClaimTypes.PreferredUserName, user.Email },
                { JwtClaimTypes.Id, user.Id.ToString() }
            },configuration ["JWT:Key"]);

            var userResp = mapper.Map<LoginResponseModel>(user);
            return GenerateSuccessResponse(new { AuthToken = token, UserData = userResp });
        }
    }

}

