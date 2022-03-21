using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrowserGame.Data;
using BrowserGame.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Static;
using BrowserGame.ModelUtils;

namespace BrowserGame.Pages.Game
{
    public class CreatePlayerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IModelFactory _modelFactory;

        public CreatePlayerModel(ApplicationDbContext context)
        {
            _context = context;
            _modelFactory = new ModelFactory(_context);
        }

        public async Task<IActionResult> OnGet()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);
            return Page();
        }

        [BindProperty]
        public string PlayerName { get; set; }
        public Player Player { get; set; }

        [BindProperty]
        public string CapitalName { get; set; }
        public City Capital { get; set; } 
        public string VerificationError { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await PlayerNameOrCityNameAlreadyInUse()) return Page();

            Player player = await _modelFactory.CreateNewPlayerAsync(PlayerName, CapitalName, User.GetUserId());

            return Redirect($"/Game/OuterCity/{player.Capital.Id}");
        }

		private async Task<bool> PlayerNameOrCityNameAlreadyInUse()
		{
            if (await _context.Players.AnyAsync(p => p.Name == PlayerName))
            {
                VerificationError = "Player name already in use.";
                return true;
            }
            else if (await _context.Cities.AnyAsync(c => c.Name == CapitalName))
            {
                VerificationError = "City name already in use.";
                return true;
            }
            else return false;
        }

		private async Task<string> RedirectIfAlreadyCreatedAsync()
		{
            Player player = await _context.Players.Include(p => p.Capital)
											   .FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player != null) return $"/Game/OuterCity/{player.Capital.Id}";
            return null;
        }
    }
}
