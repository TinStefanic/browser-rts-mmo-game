using BrowserGame.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BrowserGame.Static
{
	public static class CityBuildingTypeInfo
	{
		private static JObject _descriptions;

		public static string Description(this CityBuildingType cityBuildingType)
		{
			_descriptions ??= LoadDescriptionsJson();
			return _descriptions[cityBuildingType.ToString()]["Description"].ToString();
		}

		public static string ValueDescription(this CityBuildingType cityBuildingType)
		{
			_descriptions ??= LoadDescriptionsJson();
			return _descriptions[cityBuildingType.ToString()]["ValueDescription"].ToString();
		}

		private static JObject LoadDescriptionsJson()
		{
			string descriptonsPath = "cityBuildingDescriptions.json";
			return JObject.Parse(File.ReadAllText(descriptonsPath));
		}
	}
}
