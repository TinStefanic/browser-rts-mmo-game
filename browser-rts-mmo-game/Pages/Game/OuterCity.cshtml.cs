using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Data;
using BrowserGame.Models;
using System.Security.Claims;
using BrowserGame.Static;
using BrowserGame.Internal;

namespace BrowserGame.Pages.Game
{
    public class OuterCityModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OuterCityModel(ApplicationDbContext context)
        {
            _context = context;
        }

        internal CityManager CityManager { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                var player = await _context.Players.Include(p => p.Capital)
                                                   .FirstOrDefaultAsync(p => p.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (player == null) return Redirect("/Game/CreatePlayer");

                id = player.Capital.Id;
            }

            CityManager = await CityManager.LoadCityManagerAsync(id ?? 0, _context);

            if (CityManager.NotUsers(User))
            {
                return NotFound();
            }

            ViewData["CityManager"] = CityManager;
            return Page();
        }
    }
}
