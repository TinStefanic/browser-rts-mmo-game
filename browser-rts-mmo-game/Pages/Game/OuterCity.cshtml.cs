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

namespace BrowserGame.Pages.Game
{
    public class OuterCityModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OuterCityModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public City City { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                var player = await _context.Players.Include(p => p.Capital)
                                                   .FirstOrDefaultAsync(p => p.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (player == null) return Redirect("/Game/CreatePlayer");

                id = player.Capital.Id;
            }

            City = await _context.Cities.Include(c => c.Clay.Fields)
                                        .Include(c => c.Wood.Fields)
                                        .Include(c => c.Iron.Fields)
                                        .Include(c => c.Crop.Fields)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (City == null)
            {
                return NotFound();
            }

            ViewData["City"] = City;
            return Page();
        }
    }
}
