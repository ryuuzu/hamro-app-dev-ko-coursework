using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Services
{
    public static class SeedData
    {
        public static async Task InitializedAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Staff"))
            {
                await roleManager.CreateAsync(new IdentityRole("Staff"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            // Create admin user
            ApplicationUser adminUser = new()
            {
                FirstName = "Hkcr",
                LastName = "Admin",
                UserName = "hkcr_admin",
                Email = "hkcr_admin@gmail.com",
                EmailConfirmed = true
            };
            // Create staff user
            ApplicationUser staffUser = new()
            {
                FirstName = "Hkcr",
                LastName = "Staff",
                UserName = "hkcr_staff",
                Email = "hkcr_staff@gmail.com",
                EmailConfirmed = true
            };
            // Create customer user
            ApplicationUser customerUser = new()
            {
                FirstName = "Hkcr",
                LastName = "Customer",
                UserName = "hkcr_customer",
                Email = "hkcr_customer@gmail.com",
                EmailConfirmed = true
            };
            const string password = "Hkcr1234!";
            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await userManager.CreateAsync(adminUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (await userManager.FindByEmailAsync(staffUser.Email) == null)
            {
                var result = await userManager.CreateAsync(staffUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, "Staff");
                }
            }

            if (await userManager.FindByEmailAsync(customerUser.Email) == null)
            {
                var result = await userManager.CreateAsync(customerUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }
        }
    }
}