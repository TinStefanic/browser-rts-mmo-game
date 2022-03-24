using Newtonsoft.Json.Linq;
using System.Numerics;

namespace BrowserGame.Static
{
	public static class TimeManager
	{
		public static long Ticks => DateTime.Now.Ticks;

		private static int? _speed = null;
		public static int Speed => _speed ??= SpeedFromJson();

		/// <summary>
		/// Returns current time in ticks and calculates time elapsed in hours.
		/// </summary>
		/// <returns>
		/// Current time in ticks.
		/// </returns>
		public static long UpdateTime(long lastUpdate, out decimal elapsedHours)
		{
			long currentTime = Ticks;
			elapsedHours = (decimal)new TimeSpan(currentTime - lastUpdate).TotalHours * Speed;
			return currentTime;
		}

		private static int SpeedFromJson()
		{
			string descriptonsPath = "gamesettings.json";
			return ((int)JObject.Parse(File.ReadAllText(descriptonsPath))["GameSpeed"]);
		}
	}
}
