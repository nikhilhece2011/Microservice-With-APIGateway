using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.AutoFac;
using Identity.API.Configurations;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureJWTToken(Configuration);
            services.AddOptions();
            services.Configure<GlobalIdentitySettings>(Configuration.GetSection("GLOBALIDENTITYSETTINGS"));
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddTransient<IdentityDbSeeder>();
            return AutoFacProviderConfigurations.ConfigureAutoFacDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IdentityDbSeeder identityDbSeeder)
        {
            app.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

            app.UseAuthentication();
            app.UseMvc();
            identityDbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}
