using POD_3.BLL.Repositories.Impl;
using POD_3.BLL.Repositories.Repository;

namespace POD_3.Core
{
    public static class Configuration
    {
        public static void RegisterProjectDependencies(this WebApplicationBuilder builder)
        {

         
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? builder.Configuration["JWT:Key"];
            builder.Services.RegisterJWTAuthentication(jwtKey);
            
        }
    }
}
