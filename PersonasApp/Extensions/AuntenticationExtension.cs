using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Options;
using PersonasApp.Entities;
using PersonsApp.Database;

namespace PersonasApp.Extensions
{
    public static class AuntenticationExtension
    {
        public static void AddAuthenticationconfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<UserEntity,RoleEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = "EmailConfirmation";
            }).AddEntityFrameworkStores<PersonsDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                
            });
        }
    }
}