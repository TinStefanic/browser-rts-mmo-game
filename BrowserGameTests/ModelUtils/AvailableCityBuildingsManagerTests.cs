using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.ModelUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BrowserGame.Models;

namespace BrowserGame.ModelUtils.Tests
{
	[TestClass()]
	public class AvailableCityBuildingsManagerTests
	{
		[TestMethod()]
		public void ShouldReturnWarehouseAndNotGranaryTest()
		{
			var moq = new Mock<ICityManager>();
			moq.Setup(m => m.BuildingSlots).Returns(CreateBuildingSlotsWithOnlyGranary());
			var availableCityBuildingsManager = new AvailableCityBuildingsManager(moq.Object);
			Assert.IsTrue(availableCityBuildingsManager.IsAvailable(CityBuildingType.Warehouse));
			Assert.IsFalse(availableCityBuildingsManager.IsAvailable(CityBuildingType.Granary));
		}

		private static IList<CityBuilding> CreateBuildingSlotsWithOnlyGranary()
		{
			var list = new List<CityBuilding>
			{
				new CityBuilding(null, CityBuildingType.Granary)
			};
			for (int i = 1; i < BuildingSlot.NumBuildingSlots; ++i)
			{
				list.Add(new CityBuilding(null, CityBuildingType.EmptySlot));
			}
			return list;
		}
	}
}