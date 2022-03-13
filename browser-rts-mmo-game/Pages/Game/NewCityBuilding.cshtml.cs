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
        }

        public CityBuilding CityBuilding { get; set; }

        public IList<UpgradeInfo> UpgradeInfo { get; set; }

        [BindProperty]
        public int CityBuildingId { get; set; }
        [BindProperty]
        public CityBuildingType CityBuildingType { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding cityBuilding = await _context.CityBuildings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (cityBuilding?.CityBuildingType != CityBuildingType.EmptySlot) return BadRequest();

            _cityManager = await CityManager.LoadCityManagerAsync(cityBuilding.CityId ?? 0, _context);

            ViewData["CityManager"] = _cityManager;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding = await _context.CityBuildings
                                          .Include(cb => cb.City)
                                          .FirstOrDefaultAsync(cb => cb.Id == CityBuildingId);

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
