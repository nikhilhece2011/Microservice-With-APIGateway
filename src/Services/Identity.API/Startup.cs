using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.API.AutoFac;
using Identity.API.Configurations;
using Identity.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureJWTToken(Configuration);
            services.AddOptions();
            services.Configure<GlobalIdentitySettings>(Configuration.GetSection("GLOBALIDENTITYSETTINGS"));
            return AutoFacProviderConfigurations.ConfigureAutoFacDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
