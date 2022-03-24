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
using BrowserGame.Static;
using BrowserGame.ModelUtils;

namespace BrowserGame.Pages.Game
{
    public class NewMessageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewMessageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Player player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (player == null) return Redirect("/Game/CreatePlayer");

            ViewData["CityManager"] = await CityManager.LoadCityManagerAsync(player.ActiveCityId, _context);

            return Page();
        }

        [BindProperty]
        public string Recipient { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string MessageBody { get; set; }

        public string VerificationErrorMessage { get; set; }
        public Message Message { get; set; } // For verification only.

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Player thisPlayer = await _context.Players.FirstOrDefaultAsync(p => p.UserId == User.GetUserId());

            if (thisPlayer == null) return Redirect("/Game/CreatePlayer");

            Player recipientPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Name == Recipient);

            if (recipientPlayer == null)
			{
                VerificationErrorMessage = $"Player named {Recipient} doesn't exist";
                return Page();
			}

            var message = new Message()
            {
                RecipientName = Recipient,
                RecipientId = recipientPlayer.Id,
                Title = Title,
                MessageBody = MessageBody,
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
