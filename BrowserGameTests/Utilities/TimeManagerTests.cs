using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BrowserGame.Utilities.Tests
{
	[TestClass()]
	public class TimeManagerTests
	{
		[TestMethod()]
		public async Task Elapsed_Hours_Should_Be_Greater_Than_Zero_Test()
		{
			typeof(TimeManager).GetField("_speed", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);

			long oldTime = TimeManager.Ticks;
			await Task.Delay(1);
			long newTime = TimeManager.UpdateTime(oldTime, out decimal elapsedHours);

			Assert.IsTrue(elapsedHours > 0m);
			Assert.IsTrue(newTime > oldTime);
		}
	}
}