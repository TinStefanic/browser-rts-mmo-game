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

        public ResourceField ResourceField { get; set; }
        public UpgradeInfo UpgradeInfo { get; set; }

        internal CityManager CityManager { get; set; }

        [BindProperty]
        public int FieldId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ResourceField = await _context.ResourceFields.Include(rf => rf.City.Player).FirstOrDefaultAsync(rf => rf.Id == id);
            UpgradeInfo = await _context.UpgradeInfos.FindAsync(GameSession.GetUpgradeInfoId(ResourceField.Name, ResourceField.Level));

            CityManager = await CityManager.LoadCityManagerAsync(ResourceField.City.Id, _context);
            ViewData["CityManager"] = CityManager;
            ViewData["ResourceField"] = ResourceField;
            ViewData["UpgradeInfo"] = UpgradeInfo;

            if (CityManager.NotUsers(User))
                return NotFound();

            FieldId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
            ResourceField = await _context.ResourceFields
                                          .Include(rf => rf.City)
                                          .FirstOrDefaultAsync(rf => rf.Id == FieldId);

            CityManager = await CityManager.LoadCityManagerAsync(ResourceField.City.Id, _context);

            if (CityManager.NotUsers(User))
                return NotFound();

            if (await CityManager.TryUpgradeAsync(ResourceField))
                return Redirect($"Game/OuterCity/{CityManager.Id}");
            else
                return Page();
        }
    }
}
