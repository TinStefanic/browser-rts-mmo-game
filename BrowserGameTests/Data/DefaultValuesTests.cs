using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.Models;

namespace BrowserGame.Data.Tests
{
	[TestClass()]
	public class DefaultValuesTests
	{
		[TestMethod()]
		public void All_Default_Values_Should_Be_Implemented_Test()
		{
			foreach (CityBuildingType cityBuildingType in Enum.GetValues(typeof(CityBuildingType)))
			{
				// Empty slot doesn't have and doesn't need default value.
				if (cityBuildingType == CityBuildingType.EmptySlot) continue;

				try { cityBuildingType.GetDefaultValue(); }
				catch (ArgumentException ex) { Assert.Fail(ex.Message); }
			}
		}
	}
}