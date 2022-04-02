using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGameTests
{
	public static class TestConfigurationFactory
	{
		private static IConfiguration _configuration;

		public static IConfiguration CreateConfiguration()
		{
			return _configuration ??= InitConfiguration();
		}

		private static IConfiguration InitConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Test.json")
				.Build();
		}
	}
}
