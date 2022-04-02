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

namespace BrowserGame.Pages.Game
{
    public class NewCityBuildingModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		private City _city;

        public NewCityBuildingModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
			BuildingInfoFactory = new BuildingInfoFactory(_context);
        }

        public CityBuilding CityBuilding { get; set; }
        [BindProperty]
        public int CityBuildingId { get; set; }
        [BindProperty]
        public CityBuildingType CityBuildingType { get; set; }
        public IEnumerable<CityBuildingType> AvailableCityBuildings { get; set; }

        public IBuildingInfoFactory BuildingInfoFactory { get; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CityBuildingId = id;

            CityBuilding = await _context.CityBuildings.FirstOrDefaultAsync(cb => cb.Id == id);

            if (CityBuilding?.CityBuildingType != CityBuildingType.EmptySlot) return BadRequest();

            _city = await new ModelFactory(_context, _configuration).LoadCityAsync(CityBuilding.CityId ?? 0);

            if (_city.NotUsers(User)) return Forbid();

            ViewData["City"] = _city;

            AvailableCityBuildings = new AvailableCityBuildings(_city).AvailableBuildings;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CityBuilding = await _context.CityBuildings.FirstOrDefaultAsync(cb => cb.Id == CityBuildingId);

            if (CityBuilding == null) return NotFound();

            _city = await new ModelFactory(_context, _configuration).LoadCityAsync(CityBuilding?.CityId ?? 0);

            if (_city.NotUsers(User)) return Forbid();

            if (! new AvailableCityBuildings(_city).IsAvailable(CityBuildingType)) return BadRequest();

            var buildingConstrutor = new BuildingConstructor(_city, _context);
            if (await buildingConstrutor.TryCreateBuildingAsync(CityBuilding, CityBuildingType))
                return Redirect($"/Game/InnerCity/{_city.Id}");
            else
                return Page();
        }
    }
}
