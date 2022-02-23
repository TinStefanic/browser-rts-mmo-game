namespace BrowserGame.Static
{
	public static class TimeManager
	{
		public static long Ticks => Speed * DateTime.Now.Ticks;

		// Set from appsettings.json in Program.cs.
		public static int Speed { get; set; }
	}
}
