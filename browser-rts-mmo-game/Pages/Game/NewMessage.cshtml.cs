using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrowserGame.Data;
using BrowserGame.Models;
using Microsoft.EntityFrameworkCore;
using BrowserGame.Utilities;
using BrowserGame.ModelUtils;

namespace BrowserGame.Pages.Game
{
    public class NewMessageModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public NewMessageModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
			_configuration = configuration;
		}

        public async Task<IActionResult> OnGetAsync()
        {
            Player player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player == null) return Redirect("/Game/CreatePlayer");

            ViewData["City"] = await new ModelFactory(_context, _configuration).LoadCityAsync(player.ActiveCityId);

            return Page();
        }

        public string VerificationErrorMessage { get; set; }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string recipient = Message.RecipientName;
            string title = Message.Title;
            string messageBody = Message.MessageBody;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Player thisPlayer = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());
            if (thisPlayer == null) return Redirect("/Game/CreatePlayer");
            ViewData["City"] = await new ModelFactory(_context, _configuration).LoadCityAsync(thisPlayer.ActiveCityId);

            Player recipientPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Name == recipient);

            if (recipientPlayer == null)
			{
                VerificationErrorMessage = $"Player named {recipient} doesn't exist";
                return Page();
			}

            var message = new Message()
            {
                RecipientName = recipient,
                RecipientId = recipientPlayer.Id,
                Title = title,
                MessageBody = messageBody,
                SenderName = thisPlayer.Name,
                SenderId = thisPlayer.Id,
                SentAt = DateTime.Now,
                Unread = true
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
