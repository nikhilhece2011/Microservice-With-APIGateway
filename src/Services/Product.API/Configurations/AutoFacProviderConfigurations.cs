using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Product.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Configurations
{
    public class AutoFacProviderConfigurations
    {
        public static IServiceProvider ConfigureAutoFacDependencies(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterType(typeof(ProductContext)).InstancePerDependency();
            return new AutofacServiceProvider(container.Build());
        }
    }
}
