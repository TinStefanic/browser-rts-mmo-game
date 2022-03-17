using BrowserGame.Data;
using BrowserGame.Static;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGameTests
{
	public static class TestDbConntextFactory
	{
		private static readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;" +
														   "Database=aspnet-browser_rts_mmo_game-test-53bc9b9d-9d6a-45d4-8429-2a2761773502;" +
														   "Trusted_Connection=True;MultipleActiveResultSets=true";

		private static bool _initialized = false;

		public static async Task<ApplicationDbContext> CreateContextAsync()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>();
			options.UseSqlServer(_connectionString);
			
			var context = new ApplicationDbContext(options.Options);

			if (!_initialized)
			{
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();

				await DbInitializer.InitializeAsync(context);
				await TestDbInitializer.SeedAsync(context);

				// Speed should be large enough to ensure build times last 0 seconds.
				typeof(TimeManager).GetProperty("Speed").SetValue(null, 3600);

				_initialized = true;
			}

			return context;
		}
	}
}
