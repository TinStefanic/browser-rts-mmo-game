using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Player
    {
        public int Id { get; set; }

        [Required, ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        [Required, StringLength(64, MinimumLength = 3)]
        public string Name { get; set; }

        [ForeignKey("City")]
        public int? CapitalId { get; set; }
        public City Capital { get; set; }
    }
}
