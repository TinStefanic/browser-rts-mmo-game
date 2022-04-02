using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.ModelUtils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGameTests
{
	public class TestDbInitializer
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			var modelFactory = new ModelFactory(context, TestConfigurationFactory.CreateConfiguration());

			if (context.Players.Any()) return;

			// Id's start with 1.
			await modelFactory.CreateNewPlayerAsync("testPlayer", "testCapital", "random-test-user-id");

			await context.SaveChangesAsync();
		}
	}
}
