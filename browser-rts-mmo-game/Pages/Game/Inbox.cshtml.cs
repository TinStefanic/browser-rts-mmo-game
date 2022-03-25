using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.ModelUtils;
using BrowserGame.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Pages.Game
{
    public class InboxModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

		public InboxModel(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
            _configuration = configuration;
		}

        public PaginatedList<Message> Messages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            Player player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player == null) return Redirect("/Game/CreatePlayer");

            ViewData["City"] = await new ModelFactory(_context).LoadCityAsync(player.ActiveCityId);

            var pageSize = _configuration.GetValue("PageSize", 10);
            Messages = await PaginatedList<Message>.CreateAsync(
                _context.Messages
                    .AsQueryable()
                    .Where(m => m.RecipientId == player.Id)
                    .OrderByDescending(m => m.SentAt)
                    .AsNoTracking(),
                pageIndex ?? 1,
                pageSize
            );

            return Page();
        }
    }
}
