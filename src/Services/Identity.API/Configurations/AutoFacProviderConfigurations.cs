using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.API.Infrastructure;
using Identity.API.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.AutoFac
{
    public class AutoFacProviderConfigurations
    {
        public static IServiceProvider ConfigureAutoFacDependencies(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterType(typeof(IdentityDbContext)).InstancePerDependency();
            container.RegisterType<JWTService>().As<IJWTService>().InstancePerDependency();
            return new AutofacServiceProvider(container.Build());
        }
    }
}
