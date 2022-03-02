using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BrowserGame.Pages.Game
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/Game/CreatePlayer");
		}
    }
}
