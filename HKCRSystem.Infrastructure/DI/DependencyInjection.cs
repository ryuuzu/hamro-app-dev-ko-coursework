using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Domain.Entities;
using HKCRSystem.Infrastructure.Persistence;
using HKCRSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKCRSystem.Infrastructure.Email;

namespace HKCRSystem.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //gets the jwt config and secret key from appsettings
            var jwtConfig = configuration.GetSection("Jwt");
            var secretKey = jwtConfig["Key"];
            
            services.AddDbContext<ApplicationDBContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("HKCRDatabase"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            //validates token for 10 hours
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(10));
            
            //adding authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig["Issuer"],
                        ValidAudience = jwtConfig["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());

            // Add Services here.
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUserAuthentication, UserAuthenticationService>();
            services.AddTransient<IUserManagement, UserManagementService>();
            services.AddTransient<IOffer, OfferService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IGmailEmailProvider, GmailEmailProvider>();

            return services;
        }
    }
}
