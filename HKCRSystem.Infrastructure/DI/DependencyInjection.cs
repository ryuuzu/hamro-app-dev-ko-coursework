using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Infrastructure.Persistence;
using HKCRSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(
                options =>
                options.UseNpgsql(configuration.GetConnectionString("HKCRDatabase"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)),
                ServiceLifetime.Transient);
            services.AddScoped<IApplicationDBContext>(provider =>
                (IApplicationDBContext)provider.GetServices<ApplicationDBContext>()
            );
            // Add Services here.
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}
