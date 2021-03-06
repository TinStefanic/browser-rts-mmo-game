using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using BrowserGame.Models;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class BuildingInfoFactoryTests
	{
		[TestMethod()]
		public async Task Should_Create_New_Building_Info_For_Wall_Test()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var buildingInfo = 
				await new BuildingInfoFactory(context).CreateNewBuildingInfoAsync(CityBuildingType.Wall);

			Assert.IsNotNull(buildingInfo);
			Assert.AreEqual(CityBuildingType.Wall.ToString(), buildingInfo.BuildingName);
			Assert.IsTrue(buildingInfo.IronCost > 0);
		}
	}
}