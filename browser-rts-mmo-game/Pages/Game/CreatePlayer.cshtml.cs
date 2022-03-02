using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrowserGame.Data;
using BrowserGame.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Static;
using BrowserGame.Utility;

namespace BrowserGame.Pages.Game
{
    public class CreatePlayerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreatePlayerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);
            return Page();
        }

        [BindProperty]
        public string PlayerName { get; set; }
        public Player Player { get; set; }

        [BindProperty]
        public string CapitalName { get; set; }
        public City Capital { get; set; } 


        public async Task<IActionResult> OnPostAsync()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newPlayer = new Player()
            {
                UserId = GameSession.GetUserId(User),
                Name = PlayerName
            };

            _context.Players.Add(newPlayer);

            var newCity = new City()
            {
                Name = CapitalName,
                Player = newPlayer
            };

            _context.Cities.Add(newCity);

            AddResources(newCity);

            _context.BuildQueues.Add(newCity.BuildQueue);

            newPlayer.Capital = newCity;
            await _context.SaveChangesAsync();

            return Redirect($"Game/OuterCity/{newCity.Id}");
        }

        private void AddResources(City newCity)
		{
            _context.Clays.Add(newCity.Clay);
            foreach (ResourceField rf in newCity.Clay.Fields)
			{
                rf.City = newCity;
				_context.ResourceFields.Add(rf);
			}

			_context.Irons.Add(newCity.Iron);
            foreach (ResourceField rf in newCity.Iron.Fields)
			{
                rf.City = newCity;
                _context.ResourceFields.Add(rf);
			}

			_context.Woods.Add(newCity.Wood);
            foreach (ResourceField rf in newCity.Wood.Fields)
			{
                rf.City = newCity;
                _context.ResourceFields.Add(rf);
			}

			_context.Crops.Add(newCity.Crop);
            foreach (ResourceField rf in newCity.Crop.Fields)
			{
                rf.City = newCity;
                _context.ResourceFields.Add(rf);
			}
		}

        private async Task<string> RedirectIfAlreadyCreatedAsync()
		{
            Player player = await _context.Players.Include(p => p.Capital)
											   .FirstOrDefaultAsync(p => p.UserId == GameSession.GetUserId(User));

            if (player != null) return $"/Game/OuterCity/{player.Capital.Id}";
            return null;
        }
    }
}
