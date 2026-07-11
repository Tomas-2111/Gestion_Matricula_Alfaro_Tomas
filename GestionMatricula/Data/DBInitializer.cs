using Microsoft.AspNetCore.Identity;

namespace GestionMatricula.Data
{
    public class DBInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //crear un user admin
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminPassword, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    //AGREGAR CLAIMS

                    await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Department", "IT"));
                    await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("Permission", "ViewDashboard"));

                }
            }

        }
    }
}
