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
    public class InnerCityModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public InnerCityModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? recivedId)
        {
            Player player = await _context.Players.Include(p => p.Capital)
                                                   .FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player == null) return Redirect("/Game/CreatePlayer");

            int id = recivedId ?? player.ActiveCityId;

            City city = await new ModelFactory(_context).LoadCityAsync(id);

            if (city.NotUsers(User))
            {
                return Forbid();
            }

            player.ActiveCityId = id;
            await _context.SaveChangesAsync();
            ViewData["City"] = city;
            return Page();
        }
    }
}
