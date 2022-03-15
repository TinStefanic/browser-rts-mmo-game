using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.ModelUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGameTests
{
	public class TestDbInitializer
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			var modelFactory = new ModelFactory(context);

			if (context.Players.Any()) return;

			// Id's start with 1.
			await modelFactory.CreateNewPlayerAsync("testPlayer", "testCapital", "random-test-user-id");

			UpgradeInfo upgradeInfo = GetLvl0CropUpgradeInfo();

			await context.UpgradeInfos.AddAsync(upgradeInfo);

			await context.SaveChangesAsync();
		}

		public static UpgradeInfo GetLvl0CropUpgradeInfo()
		{
			return new UpgradeInfo
			{
				ClayCost = 100,
				CropCost = 100,
				AdditionalCropUpkeep = 0,
				IronCost = 100,
				WoodCost = 100,
				BuildingName = "Crop",
				Level = 0,
				IsFinnalUpgrade = false,
				UpgradeDuration = 60
			};
		}
	}
}
