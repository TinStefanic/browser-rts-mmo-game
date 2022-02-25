using BrowserGame.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrowserGame.Pages.Game
{
    public class UpgradeFieldModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpgradeFieldModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? id)
        {

        }
    }
}
