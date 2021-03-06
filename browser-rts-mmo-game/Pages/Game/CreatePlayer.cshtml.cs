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
using BrowserGame.Utilities;
using BrowserGame.ModelUtils;

namespace BrowserGame.Pages.Game
{
    public class CreatePlayerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly IModelFactory _modelFactory;

        public CreatePlayerModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
			_modelFactory = new ModelFactory(_context, _configuration);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; }

        [BindProperty]
        public City Capital { get; set; } 
        public string VerificationErrorMessage { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            string redirect = await RedirectIfAlreadyCreatedAsync();
            if (redirect != null) return Redirect(redirect);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await PlayerNameOrCityNameAlreadyInUse()) return Page();

            Player player = await _modelFactory.CreateNewPlayerAsync(Player.Name, Capital.Name, User.GetUserId());

            return Redirect($"/Game/OuterCity/{player.Capital.Id}");
        }

		private async Task<bool> PlayerNameOrCityNameAlreadyInUse()
		{
            if (await _context.Players.AnyAsync(p => p.Name == Player.Name))
            {
                VerificationErrorMessage = "Player name already in use.";
                return true;
            }
            else if (await _context.Cities.AnyAsync(c => c.Name == Capital.Name))
            {
                VerificationErrorMessage = "City name already in use.";
                return true;
            }
            else return false;
        }

		private async Task<string> RedirectIfAlreadyCreatedAsync()
		{
            Player player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player != null) return $"/Game/OuterCity/{player.ActiveCityId}";
            return null;
        }
    }
}
