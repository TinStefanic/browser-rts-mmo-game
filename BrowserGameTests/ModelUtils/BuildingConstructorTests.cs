using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGameTests;
using BrowserGame.Utilities;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class BuildingConstructorTests
	{
		private ApplicationDbContext _context;
		private City _city;
		private UpgradeManager _upgradeManager;
		private BuildingConstructor _buildingConstructor;

		[TestInitialize()]
		public async Task InitializeAsync()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_city = await new ModelFactory(_context).LoadCityAsync(1);
			_upgradeManager = new UpgradeManager(_context);
			_buildingConstructor = new BuildingConstructor(_city, _context);
		}

		[TestCleanup()]
		public async Task CleanupAsync()
		{
			await _context.DisposeAsync();
		}

		[TestMethod()]
		public async Task ShouldUpgradeCropFieldToLevel1Test()
		{
			var cropField = _city.Crop.Fields.First();
			var upgrade = await _context.Upgrades.FindAsync(cropField.GetUpgradeId());
			Assert.IsTrue(await _buildingConstructor.CanUpgradeAsync(upgrade));
			Assert.IsTrue(await _buildingConstructor.TryUpgradeAsync(cropField));
			await _upgradeManager.FinishUpgradeAsync(cropField.Id, cropField.BuildingType);
			Assert.AreEqual(1, _city.Crop.Fields.First().Level);
		}

		[TestMethod()]
		public async Task EveryBuildingsUpgradeShouldBeInDatabaseTest()
		{
			foreach (CityBuildingType cityBuildingType in Enum.GetValues(typeof(CityBuildingType)))
			{
				if (cityBuildingType != CityBuildingType.EmptySlot)
				{
					Assert.IsNotNull(await _context.Upgrades.FindAsync(cityBuildingType.GetUpgradeId()));
				}
			}
		}
	}
}