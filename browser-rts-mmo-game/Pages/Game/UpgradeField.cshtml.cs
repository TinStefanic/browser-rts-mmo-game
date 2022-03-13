using BrowserGame.Data;
using BrowserGame.Internal;
using BrowserGame.Models;
using BrowserGame.Static;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Pages.Game
{
    public class UpgradeFieldModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpgradeFieldModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int FieldId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ResourceField resourceField = await _context.ResourceFields.Include(rf => rf.City.Player).FirstOrDefaultAsync(rf => rf.Id == id);
            UpgradeInfo upgradeInfo = await _context.UpgradeInfos.FindAsync(resourceField.GetUpgradeInfoId());

            CityManager cityManager = await CityManager.LoadCityManagerAsync(resourceField.City.Id, _context);
            ViewData["CityManager"] = cityManager;
            ViewData["ResourceField"] = resourceField;
            ViewData["UpgradeInfo"] = upgradeInfo;

            if (cityManager.NotUsers(User))
                return NotFound();

            FieldId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
            ResourceField resourceField = await _context.ResourceFields
                                          .Include(rf => rf.City)
                                          .FirstOrDefaultAsync(rf => rf.Id == FieldId);

            CityManager cityManager = await CityManager.LoadCityManagerAsync(resourceField.City.Id, _context);

            if (cityManager.NotUsers(User))
                return NotFound();

            if (await cityManager.TryUpgradeAsync(resourceField))
                return Redirect($"/Game/OuterCity/{cityManager.Id}");
            else
                return Page();
        }
    }
}
