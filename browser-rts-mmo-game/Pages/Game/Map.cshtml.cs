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
using BrowserGame.Utilities;

namespace BrowserGame.Pages.Game
{
    public class MapModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public MapModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
		}

        public Map Map { get;set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public Player Player { get; set; }

        public async Task<IActionResult> OnGetAsync(int? x, int? y)
        {
            Map = new Map(_context, _configuration);

            Player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());
            if (Player == null) return Redirect("./CreatePlayer");
            ViewData["City"] = await new ModelFactory(_context, _configuration).LoadCityAsync(Player.ActiveCityId);

            if (AreValidCoordinates(x, y))
			{
                XCoord = (int) x;
                YCoord = (int) y;
            }
            else
			{
                XCoord = ((City)ViewData["City"]).XCoord;
                YCoord = ((City)ViewData["City"]).YCoord;
            }

            return Page();
        }

		private bool AreValidCoordinates(int? x, int? y)
		{
			if (x == null || y == null) return false;
            if (x < 0 || y < 0) return false;
            if (x >= _configuration.GetValue("MapWidth", 10) || y >= _configuration.GetValue("MapHeight", 10))
                return false;

            return true;
		}
	}
}
