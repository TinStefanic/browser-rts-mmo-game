using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.Models;
using BrowserGame.Data;
using BrowserGameTests;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class CityUtilitiesTests
	{
		private ApplicationDbContext _context;
		private City _city;

		[TestInitialize()]
		public async Task InitializeAsync()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_city = await new ModelFactory(_context).LoadCityAsync(1);
		}

		[TestCleanup()]
		public async Task CleanupAsync()
		{
			await _context.DisposeAsync();
		}

		[TestMethod()]
		public void BuildingSlotsInitedCorrectlyTest()
		{
			Assert.IsTrue(_city.GetBuildingSlots().Any());
			Assert.AreEqual(CityBuildingType.EmptySlot, _city.GetBuildingSlots().First().CityBuildingType);
			Assert.AreEqual(CityBuildingType.MainBuilding, _city.GetMainBuilding().CityBuildingType);
		}

		[TestMethod()]
		public async Task BuildShouldNotBeInProgressTest()
		{
			Assert.IsFalse(await _city.IsBuildInProgressAsync(_context));
		}

		[TestMethod()]
		public void GetCityBuildingShouldReturnWallTest()
		{
			Assert.AreEqual(
				CityBuildingType.Wall,
				_city.GetCityBuildingOrDefault(CityBuildingType.Wall).CityBuildingType
			);
		}

		[TestMethod()]
		public void GetCityBuildingShouldReturnNullTest()
		{
			Assert.IsNull(_city.GetCityBuildingOrDefault(CityBuildingType.Granary));
		}

		[TestMethod()]
		public void ContainsCityBuildingShouldBeTrueTest()
		{
			Assert.IsTrue(_city.ContainsCityBuilding(CityBuildingType.MainBuilding));
		}

		[TestMethod()]
		public void ContainsCityBuildingShouldBeFalseTest()
		{
			Assert.IsFalse(_city.ContainsCityBuilding(CityBuildingType.Warehouse));
		}

		[TestMethod()]
		public void GetCityBuildingValueShouldReturn1Test()
		{
			var roundedReturnValue =
				decimal.Round(_city.GetCityBuildingValue(CityBuildingType.MainBuilding), 10);
			Assert.AreEqual(1m, roundedReturnValue);
		}

		[TestMethod()]
		public void GetCityBuildingValueShouldReturnDefaultValueTest()
		{
			var roundedReturnValue =
				decimal.Round(_city.GetCityBuildingValue(CityBuildingType.Warehouse), 10);
			Assert.AreEqual(DefaultValues.GetDefaultValue(CityBuildingType.Warehouse), roundedReturnValue);
		}
	}
}