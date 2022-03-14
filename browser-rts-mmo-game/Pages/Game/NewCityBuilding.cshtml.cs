using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Internal;

namespace BrowserGame.Pages.Game
{
    public class NewCityBuildingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private CityManager _cityManager;

        public NewCityBuildingModel(ApplicationDbContext context)
        {
            _context = context;
            BuildingInfoFactory = new BuildingInfoFactory(_context);
        }

        public CityBuilding CityBuilding { get; set; }
        [BindProperty]
        public CityBuildingType CityBuildingType { get; set; }
        public IEnumerable<CityBuildingType> AvailableCityBuildings { get; set; }

        internal BuildingInfoFactory BuildingInfoFactory { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding = await _context.CityBuildings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (CityBuilding?.CityBuildingType != CityBuildingType.EmptySlot) return BadRequest();

            _cityManager = await CityManager.LoadCityManagerAsync(CityBuilding.CityId ?? 0, _context);

            ViewData["CityManager"] = _cityManager;

            AvailableCityBuildings = new AvailableCityBuildingsManager(_cityManager).AvailableBuildings;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CityBuilding?.CityBuildingType != CityBuildingType.EmptySlot) return BadRequest();

            _cityManager = await CityManager.LoadCityManagerAsync(CityBuilding.City.Id, _context);

            if (_cityManager.NotUsers(User))
                return NotFound();

            if (await _cityManager.TryCreateBuildingAsync(CityBuilding, CityBuildingType))
                return Redirect($"/Game/InnerCity/{_cityManager.Id}");
            else
                return Page();
        }
    }
}
