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
		private City _city;
		private UpgradeManager _upgradeManager;

		[TestInitialize()]
		public async Task InitializeAsync()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_city = await new ModelFactory(_context, TestConfigurationFactory.CreateConfiguration()).LoadCityAsync(1);
			_upgradeManager = new UpgradeManager(_context);
		}

		[TestCleanup()]
		public async Task CleanupAsync()
		{
			await _context.DisposeAsync();
		}

		[TestMethod()]
		public async Task Should_Change_From_Not_In_Progress_Test()
		{
			Assert.AreEqual(BuildQueueStatus.Empty, _city.BuildQueue.QueueStatus);
			await _upgradeManager.StartUpgradeAsync(_city.GetMainBuilding(), _city);

			Assert.AreNotEqual(BuildQueueStatus.Empty, _city.BuildQueue.QueueStatus);
			Assert.IsTrue(_city.GetMainBuilding().IsUpgradeInProgress);
		}

		[TestMethod()]
		public async Task Should_Increase_Level_By_1_Test()
		{
			int buildingLevel = _city.GetWall().Level;
			await _upgradeManager.FinishUpgradeAsync(_city.GetWall().Id, _city.GetWall().BuildingType);

			Assert.AreEqual(buildingLevel+1, _city.GetWall().Level);
		}

		[TestMethod()]
		public async Task Should_Decrease_Building_Time_Test()
		{
			decimal oldBuildingSpeed = _city.GetBuildingSpeed();
			var buildingConstructor = new BuildingConstructor(_city, _context);
			// NOTE: This method saves changes to database.
			Assert.IsTrue(await buildingConstructor.TryUpgradeAsync(_city.GetMainBuilding()));
			// Delay is needed to ensure build completes (even if build duration is 0 seconds).
			await Task.Delay(50);

			Assert.IsFalse(await _city.IsBuildInProgressAsync(_context));
			Assert.IsTrue(oldBuildingSpeed > _city.GetBuildingSpeed());
		}
	}
}