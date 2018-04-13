using System;
using Core.Data.Repositories;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Services;
using Core.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<UserSql>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<UserValidation>();
            services.AddSingleton<UserService>();
        }
    }
}
