using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrowserGame.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (context.Upgrades.Any()) return;

            var upgradeSeeder = new UpgradeSeeder(context);

            await UpgradeInfoSeeders.SeedResourceFieldUpgradesAsync(upgradeSeeder);
            await UpgradeInfoSeeders.SeedMainBuildingUpgradesAsync(upgradeSeeder);
            await UpgradeInfoSeeders.SeedWallUpgradesAsync(upgradeSeeder);
            await UpgradeInfoSeeders.SeedWarehouseUpgradesAsync(upgradeSeeder);
            await UpgradeInfoSeeders.SeedGranaryUpgradesAsync(upgradeSeeder);

            await context.SaveChangesAsync();
        }
    }
}
