using BrowserGame.Data;
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

		public static async Task<ApplicationDbContext> CreateContextAsync()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>();
			options.UseSqlServer(_connectionString);
			
			var context = new ApplicationDbContext(options.Options);
			await context.Database.MigrateAsync();

			await TestDbInitializer.SeedAsync(context);

			return context;
		}
	}
}
