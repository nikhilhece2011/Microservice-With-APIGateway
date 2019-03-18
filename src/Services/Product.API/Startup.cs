using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Product.API.Configurations;
using Product.API.Infrastructure;
using Product.API.Models;

namespace Product.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.Configure<GlobalIdentitySettings>(Configuration.GetSection("GLOBALIDENTITYSETTINGS"));
            services.ConfigureJWTToken(Configuration);
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddTransient<ProductDBSeeder>();
            return AutoFacProviderConfigurations.ConfigureAutoFacDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ProductDBSeeder dbSeeder)
        {
            app.UseAuthentication();
            app.UseMvc();
            dbSeeder.SeedAsync(app.ApplicationServices).Wait();
        }
    }
}
