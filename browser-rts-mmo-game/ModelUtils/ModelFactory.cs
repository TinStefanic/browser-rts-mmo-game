using BrowserGame.Data;
using BrowserGame.Models;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.ModelUtils
{
	public class ModelFactory : IModelFactory
	{
		private readonly ApplicationDbContext _context;

		public ModelFactory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Player> CreateNewPlayerAsync(string playerName, string capitalName, string userId)
		{
			var newPlayer = new Player()
			{
				UserId = userId,
				Name = playerName
			};

			_context.Players.Add(newPlayer);

			var newCity = new City()
			{
				Name = capitalName,
				Player = newPlayer
			};

			_context.Cities.Add(newCity);

			_context.Clays.Add(newCity.Clay);
			_context.ResourceFields.AddRange(newCity.Clay.Fields);
			_context.Irons.Add(newCity.Iron);
			_context.ResourceFields.AddRange(newCity.Iron.Fields);
			_context.Woods.Add(newCity.Wood);
			_context.ResourceFields.AddRange(newCity.Wood.Fields);
			_context.Crops.Add(newCity.Crop);
			_context.ResourceFields.AddRange(newCity.Crop.Fields);

			_context.BuildQueues.Add(newCity.BuildQueue);
			_context.BuildingSlots.Add(newCity.BuildingSlot);
			_context.CityBuildings.AddRange(newCity.BuildingSlot.CityBuildings);

			newPlayer.Capital = newCity;
			newPlayer.ActiveCityId = newCity.Id;
			await _context.SaveChangesAsync();

			return newPlayer;
		}

		public async Task<City> LoadCityAsync(int cityId)
		{
			City city = 
				await _context.Cities
				.Include(c => c.Clay.Fields)
				.Include(c => c.Wood.Fields)
				.Include(c => c.Iron.Fields)
				.Include(c => c.Crop.Fields)
				.Include(c => c.Player)
				.Include(c => c.BuildQueue)
				.Include(c => c.BuildingSlot.CityBuildings)
				.FirstOrDefaultAsync(m => m.Id == cityId);

			await city.UpdateBuildQueueAsync(_context);

			return city;
		}
	}
}
