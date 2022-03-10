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

        public NewCityBuildingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        internal CityManager CityManager { get; set; }

        public IList<UpgradeInfo> UpgradeInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding cityBuilding = await _context.CityBuildings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (cityBuilding?.CityBuildingType != CityBuildingType.EmptySlot) return BadRequest();

            CityManager = await CityManager.LoadCityManagerAsync(cityBuilding.CityId ?? 0, _context);

            ViewData["CityManager"] = CityManager;

            return Page();
        }
    }
}
