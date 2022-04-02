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
    public class OpenMessageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public OpenMessageModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
		}

        public Message Message { get; set; }
        public int? PageIndex { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int? pageIndex)
        {
            PageIndex = pageIndex;

            Message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

            if (Message == null)
            {
                return NotFound();
            }

            Message.Unread = false;
            await _context.SaveChangesAsync();

			Player player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());
            if (player == null) return Redirect("./CreatePlayer");
            ViewData["City"] = await new ModelFactory(_context, _configuration).LoadCityAsync(player.ActiveCityId);

            return Page();
        }
    }
}
