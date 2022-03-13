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
        public int FieldId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding cityBuilding = await _context.CityBuildings.Include(cb => cb.City.Player).FirstOrDefaultAsync(cb => cb.Id == id);
            UpgradeInfo upgradeInfo = await _context.UpgradeInfos.FindAsync(cityBuilding.GetUpgradeInfoId());

			CityManager cityManager = await CityManager.LoadCityManagerAsync(cityBuilding.City.Id, _context);

            ViewData["CityManager"] = cityManager;
            ViewData["CityBuilding"] = cityBuilding;
            ViewData["UpgradeInfo"] = upgradeInfo;

            if (cityManager.NotUsers(User))
                return NotFound();

            FieldId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding cityBuilding = await _context.CityBuildings
                                          .Include(cb => cb.City)
                                          .FirstOrDefaultAsync(cb => cb.Id == FieldId);

            CityManager cityManager = await CityManager.LoadCityManagerAsync(cityBuilding.City.Id, _context);

            if (cityManager.NotUsers(User))
                return NotFound();

            if (await cityManager.TryUpgradeAsync(cityBuilding))
                return Redirect($"/Game/InnerCity/{cityManager.Id}");
            else
                return Page();
        }
    }
}
