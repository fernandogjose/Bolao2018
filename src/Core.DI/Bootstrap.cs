using System;
using Core.Data.Repositories;
using Core.Data.Sql;
using Core.Domain.Helpers;
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
            services.AddSingleton<UserGameSql>();
            services.AddSingleton<OficialGameSql>();
            
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserGameRepository, UserGameRepository>();
            services.AddSingleton<IOficialGameRepository, OficialGameRepository>();
            
            services.AddSingleton<UserValidation>();
            services.AddSingleton<UserGameValidation>();
            
            services.AddSingleton<UserService>();
            services.AddSingleton<UserGameService>();
            services.AddSingleton<OficialGameService>();
        }
    }
}
