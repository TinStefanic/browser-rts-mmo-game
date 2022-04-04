using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.ModelUtils;

namespace BrowserGame.Utilities.Tests
{
	[TestClass()]
	public class HtmlColorsTests
	{
		[TestMethod()]
		public void All_Map_Location_Types_Should_Have_Corresponding_Color()
		{
			var htmlColors = new HtmlColors();

			foreach (MapLocationType mapLocationType in Enum.GetValues(typeof(MapLocationType)))
			{
				try
				{
					htmlColors.GetColor(mapLocationType);
				}
				catch
				{
					Assert.Fail();
				}
			}
		}
	}
}