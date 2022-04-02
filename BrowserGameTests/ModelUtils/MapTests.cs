using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGameTests;
using BrowserGame.Data;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Models;
using Microsoft.Extensions.Configuration;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class MapTests
	{
		private ApplicationDbContext _context;
		private IConfiguration _configuration;
		private Map _map;
		private City _city;
		private int _mapWidth;
		private int _mapHeight;

		[TestInitialize()]
		public async Task MapTestsInit()
		{
			_context = await TestDbConntextFactory.CreateContextAsync();
			_configuration = TestConfigurationFactory.CreateConfiguration();
			_map = new Map(_context, _configuration);
			_city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == 1);
			_mapWidth = _configuration.GetValue("MapWidth", 10);
			_mapHeight = _configuration.GetValue("MapHeight", 10);
		}

		[TestMethod()]
		public async Task Returned_Coordinates_Should_Be_Different_From_Existing_City_Test()
		{
			Assert.AreNotEqual((_city.XCoord, _city.YCoord), await _map.RandomFreeCoordinatesAsync());
		}

		[TestMethod()]
		public async Task Existing_City_Coords_Should_Be_Of_Type_Owned_For_Owner()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(_city.XCoord, _city.YCoord);
			var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == 1);

			Assert.AreEqual(MapLocationType.Owned, mapLocation.GetMapLocationType(player));
		}

		[TestMethod()]
		public async Task Existing_City_Coords_Should_Be_Of_Type_Neutral_For_Neutral_Player()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(_city.XCoord, _city.YCoord);
			var player = new Player();

			Assert.AreEqual(MapLocationType.Neutral, mapLocation.GetMapLocationType(player));
		}

		// May not work if more than one player gets seeded.
		[TestMethod()]
		public async Task Adjacent_Tile_Should_Be_Of_Type_Vacant()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(_city.XCoord, _city.YCoord - 1);
			var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == 1);

			Assert.AreEqual(MapLocationType.Vacant, mapLocation.GetMapLocationType(player));
		}

		// This test assumes map size is at least 4 x 4.
		[TestMethod()]
		public async Task GetMapLocation_Should_Properly_Convert_Coordinates()
		{
			var mapLocation = await _map.GetMapLocationAtAsync(-3, -3);
			Assert.AreEqual((_mapWidth-3, _mapHeight-3), (mapLocation.XCoord, mapLocation.YCoord));

			mapLocation = await _map.GetMapLocationAtAsync(_mapWidth + 3, _mapHeight + 3);
			Assert.AreEqual((3, 3), (mapLocation.XCoord, mapLocation.YCoord));
		}
	}
}