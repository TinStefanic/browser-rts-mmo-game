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

        public CityBuilding CityBuilding { get; set; }
        public UpgradeInfo UpgradeInfo { get; set; }

        internal CityManager CityManager { get; set; }

        [BindProperty]
        public int FieldId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuilding = await _context.CityBuildings.Include(cb => cb.City.Player).FirstOrDefaultAsync(cb => cb.Id == id);
            UpgradeInfo = await _context.UpgradeInfos.FindAsync(GameSession.GetUpgradeInfoId(CityBuilding));

            CityManager = await CityManager.LoadCityManagerAsync((int)CityBuilding.City.Id, _context);
            ViewData["CityManager"] = CityManager;
            ViewData["CityBuilding"] = CityBuilding;
            ViewData["UpgradeInfo"] = UpgradeInfo;

            if (CityManager.NotUsers(User))
                return NotFound();

            FieldId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding = await _context.CityBuildings
                                          .Include(cb => cb.City)
                                          .FirstOrDefaultAsync(cb => cb.Id == FieldId);

            CityManager = await CityManager.LoadCityManagerAsync(CityBuilding.City.Id, _context);

            if (CityManager.NotUsers(User))
                return NotFound();

            if (await CityManager.TryUpgradeAsync(CityBuilding))
                return Redirect($"/Game/InnerCity/{CityManager.Id}");
            else
                return Page();
        }
    }
}
