using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using BrowserGame.Models;
using BrowserGame.Data;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class BuildingInfoTests
	{
		[TestMethod()]
		public async Task BuildDurationIsZeroTest()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(CityBuildingType.Warehouse);

			// Depends on TimeManager.Speed property, in tests Speed should be set large enough to make build time 0 seconds.
			Assert.AreEqual(0, Convert.ToInt32(buildingInfo.GetBuildDuration(await CityManager.LoadCityManagerAsync(1, context)).TotalSeconds));
		}

		[TestMethod()]
		public async Task CanBeBuiltAsyncShouldBeTrueTest()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(CityBuildingType.Warehouse);
			var cityManager = await CityManager.LoadCityManagerAsync(1, context);

			Assert.IsTrue(await buildingInfo.CanBeBuiltAsync(cityManager));
		}
	}
}