using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.Models;

namespace BrowserGame.Static.Tests
{
	[TestClass()]
	public class CityBuildingTypeInfoTests
	{
		[TestMethod()]
		public void MainBuildingAndWallShouldHaveDifferentDescriptionsTest()
		{
			string mainBuildingDescripion = CityBuildingType.MainBuilding.Description();
			string wallDescripion = CityBuildingType.Wall.Description();

			Assert.IsFalse(string.IsNullOrEmpty(mainBuildingDescripion));
			Assert.IsFalse(string.IsNullOrEmpty(wallDescripion));
			Assert.AreNotEqual(mainBuildingDescripion, wallDescripion);
		}

		[TestMethod()]
		public void GranaryAndWarehouseShouldHaveSameValueDescriptionsTest()
		{
			string granaryValueDescripion = CityBuildingType.Granary.ValueDescription();
			string warehouseValueDescription = CityBuildingType.Warehouse.ValueDescription();

			Assert.IsFalse(string.IsNullOrEmpty(granaryValueDescripion));
			Assert.IsFalse(string.IsNullOrEmpty (warehouseValueDescription));
			Assert.AreEqual(warehouseValueDescription, granaryValueDescripion);
		}

		[TestMethod()]
		public void MainBuildingAndWallShouldHaveDifferentValueDescriptionsTest()
		{
			string mainBuildingValueDescripion = CityBuildingType.MainBuilding.ValueDescription();
			string wallValueDescripion = CityBuildingType.Wall.ValueDescription();

			Assert.IsFalse(string.IsNullOrEmpty(mainBuildingValueDescripion));
			Assert.IsFalse(string.IsNullOrEmpty(wallValueDescripion));
			Assert.AreNotEqual(mainBuildingValueDescripion, wallValueDescripion);
		}

		[TestMethod()]
		public void NoneShouldBeNullTest()
		{
			foreach (CityBuildingType cityBuildingType in Enum.GetValues(typeof(CityBuildingType)))
			{
				if (cityBuildingType == CityBuildingType.EmptySlot) continue;
				Assert.IsNotNull(cityBuildingType.ValueDescription());
				Assert.IsNotNull(cityBuildingType.Description());
			}
		}
	}
}