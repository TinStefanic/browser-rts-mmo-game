using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowserGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserGame.Models;

namespace BrowserGame.Utilities.Tests
{
	[TestClass()]
	public class CityBuildingTypeInfoTests
	{
		[TestMethod()]
		public void Main_Building_And_Wall_Should_Have_Different_Descriptions_Test()
		{
			string mainBuildingDescripion = CityBuildingType.MainBuilding.Description();
			string wallDescripion = CityBuildingType.Wall.Description();

			Assert.IsFalse(string.IsNullOrEmpty(mainBuildingDescripion));
			Assert.IsFalse(string.IsNullOrEmpty(wallDescripion));
			Assert.AreNotEqual(mainBuildingDescripion, wallDescripion);
		}

		[TestMethod()]
		public void Granary_And_Warehouse_Should_Have_Same_Value_Descriptions_Test()
		{
			string granaryValueDescripion = CityBuildingType.Granary.ValueDescription();
			string warehouseValueDescription = CityBuildingType.Warehouse.ValueDescription();

			Assert.IsFalse(string.IsNullOrEmpty(granaryValueDescripion));
			Assert.IsFalse(string.IsNullOrEmpty (warehouseValueDescription));
			Assert.AreEqual(warehouseValueDescription, granaryValueDescripion);
		}

		[TestMethod()]
		public void Main_Building_And_Wall_Should_Have_Different_Value_Descriptions_Test()
		{
			string mainBuildingValueDescripion = CityBuildingType.MainBuilding.ValueDescription();
			string wallValueDescripion = CityBuildingType.Wall.ValueDescription();

			Assert.IsFalse(string.IsNullOrEmpty(mainBuildingValueDescripion));
			Assert.IsFalse(string.IsNullOrEmpty(wallValueDescripion));
			Assert.AreNotEqual(mainBuildingValueDescripion, wallValueDescripion);
		}

		[TestMethod()]
		public void None_Should_Be_Null_Test()
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