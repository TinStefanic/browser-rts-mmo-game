using BrowserGame.Data;
using BrowserGame.Models;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.ModelUtils
{
	public class Map
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly int _mapWidth;
		private readonly int _mapHeight;

		public Map(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
			_mapWidth = _configuration.GetValue("MapWidth", 10);
			_mapHeight = _configuration.GetValue("MapHeight", 10);
		}

		/// <summary>
		/// Returns tuple of (XCoord, YCoord).
		/// </summary>
		public async Task<(int, int)> RandomFreeCoordinatesAsync()
		{
			List<Tuple<int, int>> coordsInUseList = 
				await _context.Cities.Select(c => Tuple.Create(c.XCoord, c.YCoord)).ToListAsync();

			if (coordsInUseList.Count == _mapHeight * _mapWidth)
				throw new InvalidOperationException("Map is full, cannot allocate coordinates.");

			HashSet<Tuple<int, int>> coordsInUseSet = coordsInUseList.ToHashSet();

			var targetUnusedIndex = new Random().Next(_mapWidth * _mapHeight - coordsInUseList.Count);
			var numUnusedFound = 0;
			int x = 0, y = 0;

			while (numUnusedFound <= targetUnusedIndex)
			{
				if (!coordsInUseSet.Contains(Tuple.Create(x, y)))
				{
					if (numUnusedFound++ == targetUnusedIndex) break;
					
					if (++y == _mapHeight)
					{
						++x; y = 0;
					}
				}
			}

			return (x, y);
		}

		/// <summary>
		/// Returns MapLocation at coordinates (x mod MapWidth, y mod MapHeight).
		/// </summary>
		public async Task<MapLocation> GetMapLocationAtAsync(int x, int y)
		{
			x = (_mapWidth + x) % _mapWidth;
			y = (_mapHeight + y) % _mapHeight;

			var cityOrNull = await _context.Cities.FirstOrDefaultAsync(c => c.XCoord == x && c.YCoord == y);

			return new MapLocation(cityOrNull, x, y);
		}
	}
}