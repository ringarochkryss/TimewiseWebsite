using Microsoft.AspNetCore.Identity;

namespace Salto.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // seeding two roles
            string adminRole = "twAdmin";
            string customerRole = "twCustomer";
            string adminPassword = "admin1234";
            string customerPassword = "workPlanner123";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(customerRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(customerRole));
            }

            // seeding two users
            if (await userManager.FindByNameAsync("admin@timewise.se") == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@timewise.se",
                    Email = "admin@timewise.se",
                    PhoneNumber = "123345567"
                };

                var adminResult = await userManager.CreateAsync(adminUser);
                if (adminResult.Succeeded)
                {
                    await userManager.AddPasswordAsync(adminUser, adminPassword);
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }

            if (await userManager.FindByNameAsync("customer@timewise.se") == null)
            {
                var customerUser = new IdentityUser
                {
                    UserName = "customer@timewise.se",
                    Email = "customer@timewise.se",
                    PhoneNumber = "223345567"
                };

                var customerResult = await userManager.CreateAsync(customerUser);
                if (customerResult.Succeeded)
                {
                    await userManager.AddPasswordAsync(customerUser, customerPassword);
                    await userManager.AddToRoleAsync(customerUser, customerRole);
                }
            }

            // Update EmailConfirmed for existing users
            foreach (var existingUser in userManager.Users)
            {
                existingUser.EmailConfirmed = true;
                await userManager.UpdateAsync(existingUser);
            }
        }
    }
}
