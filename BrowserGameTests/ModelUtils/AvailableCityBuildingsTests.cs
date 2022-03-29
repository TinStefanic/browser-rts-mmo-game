using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BrowserGame.Models;
using BrowserGameTests;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class AvailableCityBuildingsTests
	{
		[TestMethod()]
		public async Task Should_Return_Warehouse_And_Not_Wall_Test()
		{
			using var context = await TestDbConntextFactory.CreateContextAsync();
			var city = await new ModelFactory(context).LoadCityAsync(1);
			var availableCityBuildings = new AvailableCityBuildings(city);

			Assert.IsTrue(availableCityBuildings.IsAvailable(CityBuildingType.Warehouse));
			Assert.IsFalse(availableCityBuildings.IsAvailable(CityBuildingType.Wall));
		}
	}
}