using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BrowserGame.Models
{
    public class Player
    {
        public int Id { get; set; }

        
        public int IdentityUserId { get; set; }
    }
}
