using BrowserGame.Models;

namespace BrowserGame.Data
{
	public static class UpgradeInfoSeeders
	{
        public static async Task SeedResourceFieldUpgradesAsync(UpgradeSeeder upgradeSeeder)
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
                BuildingName = typeof(Iron).Name
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
                BuildingName = typeof(Clay).Name
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
                BuildingName = typeof(Wood).Name
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
                BuildingName = typeof(Crop).Name
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }

		public static async Task SeedGranaryUpgradesAsync(UpgradeSeeder upgradeSeeder)
		{
            var upgradeSettings = new UpgradeSeederSettings
            {
                Clay = 100,
                Wood = 80,
                Iron = 60,
                Crop = 30,
                Value = 1500,
                BuildTime = 90,
                BuildingName = CityBuildingType.Granary.ToString(),
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }

		public static async Task SeedWarehouseUpgradesAsync(UpgradeSeeder upgradeSeeder)
		{
            var upgradeSettings = new UpgradeSeederSettings
            {
                Clay = 100,
                Wood = 130,
                Iron = 70,
                Crop = 50,
                Value = 1500,
                BuildTime = 110,
                BuildingName = CityBuildingType.Warehouse.ToString(),
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }

        public static async Task SeedWallUpgradesAsync(UpgradeSeeder upgradeSeeder)
		{
            var upgradeSettings = new UpgradeSeederSettings
            {
                Clay = 120,
                Wood = 90,
                Iron = 150,
                Crop = 30,
                Value = 10,
                ValueFixedChange = 10,
                UseFixedValue = true,
                BuildTime = 120,
                BuildingName = CityBuildingType.Wall.ToString(),
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }

        public static async Task SeedMainBuildingUpgradesAsync(UpgradeSeeder upgradeSeeder)
		{
            var upgradeSettings = new UpgradeSeederSettings
            {
                Clay = 150,
                Wood = 170,
                Iron = 100,
                Crop = 80,
                Value = 95,
                ValueFixedChange = -5,
                UseFixedValue = true,
                BuildTime = 150, 
                BuildingName = CityBuildingType.MainBuilding.ToString(),
            };
            await upgradeSeeder.GenerateAsync(upgradeSettings);
        }
	}
}
