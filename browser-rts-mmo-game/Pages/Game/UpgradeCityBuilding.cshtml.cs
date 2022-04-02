using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.ModelUtils;
using BrowserGame.Utilities;

namespace BrowserGame.Pages.Game
{
    public class UpgradeCityBuildingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public UpgradeCityBuildingModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
		}

        [BindProperty]
        public int CityBuildingId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding cityBuilding = await _context.CityBuildings.Include(cb => cb.City.Player).FirstOrDefaultAsync(cb => cb.Id == id);
            if (cityBuilding == null) return NotFound();

            Upgrade upgrade = await _context.Upgrades.FindAsync(cityBuilding.GetUpgradeId());

			City city = await new ModelFactory(_context, _configuration).LoadCityAsync(cityBuilding.City.Id);

            if (city.NotUsers(User)) return Forbid();

            ViewData["City"] = city;
            ViewData["CityBuilding"] = cityBuilding;
            ViewData["Upgrade"] = upgrade;

            CityBuildingId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding cityBuilding = await _context.CityBuildings
                                          .Include(cb => cb.City)
                                          .FirstOrDefaultAsync(cb => cb.Id == CityBuildingId);
            if (cityBuilding == null) return NotFound();

            City city = await new ModelFactory(_context, _configuration).LoadCityAsync(cityBuilding.City.Id);

            if (city.NotUsers(User))
                return NotFound();

            var buildingConstructor = new BuildingConstructor(city, _context);

            if (await buildingConstructor.TryUpgradeAsync(cityBuilding))
                return Redirect($"/Game/InnerCity/{city.Id}");
            else
                return Page();
        }
    }
}
