using System.Numerics;

namespace BrowserGame.Static
{
	public static class TimeManager
	{
		public static long Ticks => DateTime.Now.Ticks;

		// Set from appsettings.json in Program.cs.
		public static int Speed { get; private set; }

		/// <summary>
		/// Returns current time in ticks and calculates time elapsed in hours.
		/// </summary>
		public static long UpdateTime(long lastUpdate, out decimal elapsedHours)
		{
			long currentTime = Ticks;
			elapsedHours = (decimal)new TimeSpan(currentTime - lastUpdate).TotalHours * Speed;
			return currentTime;
		}
	}
}
