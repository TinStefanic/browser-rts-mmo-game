using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class BuildingInfoFactoryTests
	{
		[TestMethod()]
		public async Task ShouldCreateNewBuildingInfoForWallTest()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(Models.CityBuildingType.Wall);

			Assert.IsNotNull(buildingInfo);
			Assert.AreEqual(Models.CityBuildingType.Wall.ToString(), buildingInfo.BuildingName);
			Assert.IsTrue(buildingInfo.IronCost > 0);
		}
	}
}