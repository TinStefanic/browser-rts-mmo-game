using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrowserGame.Data
{
    // Based on:
    // https://stackoverflow.com/a/34791867
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            await SeedTestAdminUserAsync(context);
        }

        private static async Task SeedTestAdminUserAsync(ApplicationDbContext context)
        {
            if (context.Users.Any()) return; // Already Seeded

            var user = new IdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "email@email.com",
                NormalizedEmail = "admin@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var password = new PasswordHasher<IdentityUser>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;
            var userStore = new UserStore<IdentityUser>(context);
            await userStore.CreateAsync(user);

            await context.SaveChangesAsync();
        }
    }
}
