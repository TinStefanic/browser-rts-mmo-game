﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using System.Reflection;
using BrowserGame.Models;
using BrowserGame.Static;
using BrowserGame.Data;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class CityManagerTests
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
		public void LoadCityManagerAsyncTest()
		{
			Assert.IsNotNull(_cityManager);

			Assert.IsNotNull(_cityManager.City);

			Assert.IsNotNull(_cityManager.City.Clay.Fields);
			Assert.IsNotNull(_cityManager.City.Iron.Fields);
			Assert.IsNotNull(_cityManager.City.Wood.Fields);
			Assert.IsNotNull(_cityManager.City.Crop.Fields);
			Assert.IsNotNull(_cityManager.City.BuildingSlot?.CityBuildings);
			Assert.IsNotNull(_cityManager.City.BuildQueue);
			Assert.IsNotNull(_cityManager.City.Player);
		}

		[TestMethod()]
		public void BuildingSlotsInitedCorrectlyTest()
		{
			Assert.IsTrue(_cityManager.BuildingSlots.Any());
			Assert.AreEqual(CityBuildingType.EmptySlot, _cityManager.BuildingSlots.First().CityBuildingType);
			Assert.AreEqual(CityBuildingType.MainBuilding, _cityManager.MainBuilding.CityBuildingType);
		}

		[TestMethod()]
		public async Task BuildShouldNotBeInProgressTest()
		{
			Assert.IsFalse(await _cityManager.IsBuildInProgressAsync());
		}

		[TestMethod()]
		public async Task ShouldUpgradeCropFieldToLevel1Test()
		{
			var cropField = _cityManager.Crop.Fields.First();
			var upgradeInfo = await _context.UpgradeInfos.FindAsync(cropField.GetUpgradeInfoId());
			Assert.IsTrue(await _cityManager.CanUpgradeAsync(upgradeInfo));
			Assert.IsTrue(await _cityManager.TryUpgradeAsync(cropField));
			await new UpgradeManager(_context).FinishUpgradeAsync(cropField.Id, cropField.BuildingType);
			Assert.AreEqual(1, _cityManager.Crop.Fields.First().Level);
		}
	}
}