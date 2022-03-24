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
using BrowserGame.Static;

namespace BrowserGame.Pages.Game
{
    public class UpgradeCityBuildingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpgradeCityBuildingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int CityBuildingId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding cityBuilding = await _context.CityBuildings.Include(cb => cb.City.Player).FirstOrDefaultAsync(cb => cb.Id == id);
            if (cityBuilding == null) return NotFound();

            UpgradeInfo upgradeInfo = await _context.UpgradeInfos.FindAsync(cityBuilding.GetUpgradeInfoId());

			ICityManager cityManager = await CityManager.LoadCityManagerAsync(cityBuilding.City.Id, _context);

            if (cityManager.NotUsers(User)) return Forbid();

            ViewData["CityManager"] = cityManager;
            ViewData["CityBuilding"] = cityBuilding;
            ViewData["UpgradeInfo"] = upgradeInfo;

            CityBuildingId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding cityBuilding = await _context.CityBuildings
                                          .Include(cb => cb.City)
                                          .FirstOrDefaultAsync(cb => cb.Id == CityBuildingId);
            if (cityBuilding == null) return NotFound();

            ICityManager cityManager = await CityManager.LoadCityManagerAsync(cityBuilding.City.Id, _context);

            if (cityManager.NotUsers(User))
                return NotFound();

            if (await cityManager.TryUpgradeAsync(cityBuilding))
                return Redirect($"/Game/InnerCity/{cityManager.Id}");
            else
                return Page();
        }
    }
}
