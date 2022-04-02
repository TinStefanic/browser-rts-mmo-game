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
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class MapLocationTests
	{
		private ApplicationDbContext _context;
		private Map _map;
		private City _city;
		private Player _player;

		[TestInitialize()]
		public async Task MapTestsInit()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_map = new Map(_context, TestConfigurationFactory.CreateConfiguration());
			_city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == 1);
			_player = await _context.Players.FirstOrDefaultAsync(p => p.Id == 1);
		}

		[TestMethod()]
		public async Task MapLocation_Should_Have_Correct_Info_On_City()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(_city.XCoord, _city.YCoord);

			Assert.AreEqual(_city.XCoord, mapLocation.XCoord);
			Assert.AreEqual(_city.YCoord, mapLocation.YCoord);
			Assert.IsFalse(mapLocation.IsVacant);
			Assert.AreEqual(_city.Name, mapLocation.GetCityNameOrEmpty());
			Assert.AreEqual(MapLocationType.Owned, mapLocation.GetMapLocationType(_player));
		}

		// May not work if more than one player gets seeded.
		[TestMethod()]
		public async Task MapLocation_Should_Have_Correct_Info_On_Vacant_Tile()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(_city.XCoord, _city.YCoord - 1);

			Assert.IsTrue(mapLocation.IsVacant);
			Assert.AreEqual(string.Empty, mapLocation.GetCityNameOrEmpty());
			Assert.AreEqual(MapLocationType.Vacant, mapLocation.GetMapLocationType(_player));
		}
	}
}