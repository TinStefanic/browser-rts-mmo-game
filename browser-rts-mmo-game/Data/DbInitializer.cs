using BrowserGame.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrowserGame.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (context.UpgradeInfos.Any()) return;

            var upgradeSeeder = new UpgradeSeeder(context);

            await SeedResourceFieldUpgradesAsync(upgradeSeeder);
        }

        private static async Task SeedResourceFieldUpgradesAsync(UpgradeSeeder upgradeSeeder)
        {
            int r1 = 50, r2 = 35, r3 = 15, r4 = 20;
            int production = 5, buildTime = 60;

            var upgradeSettings = new UpgradeSeederSettings
            {
                Clay = r1,
                Wood = r2,
                Iron = r3,
                Crop = r4,
                Value = production,
                BuildTime = buildTime,
                BuildingName = "Iron"
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);

            upgradeSettings = new UpgradeSeederSettings
            {
                Clay = r3,
                Wood = r1,
                Iron = r2,
                Crop = r4,
                Value = production,
                BuildTime = buildTime,
                BuildingName = "Clay"
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);

            upgradeSettings = new UpgradeSeederSettings
            {
                Clay = r1,
                Wood = r2,
                Iron = r2,
                Crop = r4,
                Value = production,
                BuildTime = buildTime,
                BuildingName = "Wood"
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);

            upgradeSettings = new UpgradeSeederSettings
            {
                Clay = r2,
                Wood = r1,
                Iron = r3,
                Crop = r4,
                Value = production,
                BuildTime = buildTime,
                BuildingName = "Crop"
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }
    }
}
