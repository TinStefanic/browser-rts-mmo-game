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
		public async Task Build_Duration_Is_Zero_Test()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = 
				await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(CityBuildingType.Warehouse);

			var buildDuration =
				Convert.ToInt32(
					buildingInfo.GetBuildDuration(
						await new ModelFactory(
							context, 
							TestConfigurationFactory.CreateConfiguration()
						).LoadCityAsync(1)
					).TotalSeconds
				);

			// Depends on TimeManager.Speed property,
			// in tests Speed should be set large enough to make build time 0 seconds.
			Assert.AreEqual(0, buildDuration);
		}

		[TestMethod()]
		public async Task Can_Be_Built_Async_Should_Be_True_Test()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = 
				await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(CityBuildingType.Warehouse);
			var city = 
				await new ModelFactory(context, TestConfigurationFactory.CreateConfiguration()).LoadCityAsync(1);

			Assert.IsTrue(await buildingInfo.CanBeBuiltAsync(city));
		}
	}
}