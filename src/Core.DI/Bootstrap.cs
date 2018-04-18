using Core.Data.Repositories;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Services;
using Core.Domain.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DI {
    public class Bootstrap {

        public static void Configure (IServiceCollection services) {
            services.AddSingleton<OficialGameSql> ();
            services.AddSingleton<UserGameSql> ();
            services.AddSingleton<UserPointSql> ();
            services.AddSingleton<UserSql> ();

            services.AddSingleton<IOficialGameRepository, OficialGameRepository> ();
            services.AddSingleton<IUserGameRepository, UserGameRepository> ();
            services.AddSingleton<IUserPointRepository, UserPointRepository> ();
            services.AddSingleton<IUserRepository, UserRepository> ();

            services.AddSingleton<OficialGameValidation> ();
            services.AddSingleton<UserGameValidation> ();
            services.AddSingleton<UserValidation> ();

            services.AddSingleton<OficialGameService> ();
            services.AddSingleton<UserGameService> ();
            services.AddSingleton<UserPointService> ();
            services.AddSingleton<UserService> ();
        }
    }
}