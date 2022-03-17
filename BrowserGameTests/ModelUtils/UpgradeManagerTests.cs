using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Models.Misc;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class UpgradeManagerTests
	{
		private ApplicationDbContext _context;
		private ICityManager _cityManager;
		private UpgradeManager _upgradeManager;

		[TestInitialize()]
		public async Task InitializeAsync()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_cityManager = await CityManager.LoadCityManagerAsync(1, _context);
			_upgradeManager = new UpgradeManager(_context);
		}

		[TestCleanup()]
		public async Task CleanupAsync()
		{
			await _context.DisposeAsync();
		}

		[TestMethod()]
		public async Task ShouldChangeFromNotInProgressTest()
		{
			Assert.AreEqual(BuildQueueStatus.Empty, _cityManager.City.BuildQueue.QueueStatus);
			await _upgradeManager.StartUpgradeAsync(_cityManager.MainBuilding, _cityManager.City);
			Assert.AreNotEqual(BuildQueueStatus.Empty, _cityManager.City.BuildQueue.QueueStatus);
			Assert.IsTrue(_cityManager.MainBuilding.IsUpgradeInProgress);
		}

		[TestMethod()]
		public async Task ShouldIncreaseLevelBy1Test()
		{
			int buildingLevel = _cityManager.Wall.Level;
			await _upgradeManager.FinishUpgradeAsync(_cityManager.Wall.Id, _cityManager.Wall.BuildingType);
			Assert.AreEqual(buildingLevel+1, _cityManager.Wall.Level);
		}
	}
}