using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DI;
using Core.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.WebApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddCors ();
            services.AddMvc ();

            Bootstrap.Configure (services);
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCors (
                options => options.WithOrigins("//localhost:4200")
                                  .WithOrigins("//bolaocopadomundo.azurewebsites.net")
                                  .WithOrigins("//www.copadomundobolao.com.br")
                                  .WithOrigins("//copadomundobolao.com.br")
                .AllowAnyHeader ()
                .AllowAnyMethod ()
                .AllowAnyOrigin ()
            );

            app.UseMiddleware (typeof (ErrorMiddleware));

            app.UseWhen (context => !context.Request.Path.StartsWithSegments ("/api/user"), appBuilder => {
                appBuilder.ApplyUserKeyValidation ();
            });

            app.UseMvc ();
        }
    }
}