using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using System.Reflection;
using BrowserGame.Models;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class CityManagerTests
	{
		[TestMethod()]
		public async Task LoadCityManagerAsyncTest()
		{
			var cityManager = await CityManager.LoadCityManagerAsync(1, await TestDbConntextFactory.CreateContextAsync());
			Assert.IsNotNull(cityManager);

			City city = typeof(CityManager).GetField("_city", BindingFlags.NonPublic | BindingFlags.Instance).GetValue((CityManager)cityManager) as City;
			Assert.IsNotNull(city);

			Assert.IsNotNull(city.Clay.Fields);
			Assert.IsNotNull(city.Iron.Fields);
			Assert.IsNotNull(city.Wood.Fields);
			Assert.IsNotNull(city.Crop.Fields);
			Assert.IsNotNull(city.BuildingSlot?.CityBuildings);
			Assert.IsNotNull(city.BuildQueue);
			Assert.IsNotNull(city.Player);
		}

		[TestMethod()]
		public async Task BuildingSlotsTest()
		{
			var cityManager = await CityManager.LoadCityManagerAsync(1, await TestDbConntextFactory.CreateContextAsync());
			Assert.IsTrue(cityManager.BuildingSlots.Any());
			Assert.AreEqual(CityBuildingType.EmptySlot, cityManager.BuildingSlots.First().CityBuildingType);
			Assert.AreEqual(CityBuildingType.MainBuilding, cityManager.MainBuilding.CityBuildingType);
		}

		[TestMethod()]
		public async Task IsBuildInProgressAsyncTest()
		{
			var cityManager = await CityManager.LoadCityManagerAsync(1, await TestDbConntextFactory.CreateContextAsync());
			Assert.IsFalse(await cityManager.IsBuildInProgressAsync());
		}

		[TestMethod()]
		public async Task CanUpgradeAsyncTest()
		{
			var cityManager = await CityManager.LoadCityManagerAsync(1, await TestDbConntextFactory.CreateContextAsync());
			Assert.IsTrue(await cityManager.CanUpgradeAsync(TestDbInitializer.GetLvl0CropUpgradeInfo()));
		}
	}
}