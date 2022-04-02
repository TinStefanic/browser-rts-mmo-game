using BrowserGame.Data;
using BrowserGame.ModelUtils;
using BrowserGame.Models;
using BrowserGame.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Pages.Game
{
    public class UpgradeFieldModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public UpgradeFieldModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
		}

        [BindProperty]
        public int FieldId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ResourceField resourceField = await _context.ResourceFields.Include(rf => rf.City.Player).FirstOrDefaultAsync(rf => rf.Id == id);
            if (resourceField == null) return NotFound();
            Upgrade upgrade = await _context.Upgrades.FindAsync(resourceField.GetUpgradeId());

            City city = await new ModelFactory(_context, _configuration).LoadCityAsync(resourceField.City.Id);
            if (city.NotUsers(User)) return NotFound();

            ViewData["City"] = city;
            ViewData["ResourceField"] = resourceField;
            ViewData["Upgrade"] = upgrade;

            FieldId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
		{
            ResourceField resourceField = await _context.ResourceFields
                                          .Include(rf => rf.City)
                                          .FirstOrDefaultAsync(rf => rf.Id == FieldId);
            if (resourceField == null) return NotFound();

            City city = await new ModelFactory(_context, _configuration).LoadCityAsync(resourceField.City.Id);

            if (city.NotUsers(User))
                return NotFound();

            var buildingConstructor = new BuildingConstructor(city, _context);

            if (await buildingConstructor.TryUpgradeAsync(resourceField))
                return Redirect($"/Game/OuterCity/{city.Id}");
            else
                return Page();
        }
    }
}
