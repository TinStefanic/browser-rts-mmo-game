using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrowserGame.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(UserId), IsUnique = true)]
    public class Player
    {
        public int Id { get; set; }

        [StringLength(400)]
        public string UserId { get; set; }

        [Display(Name = "Player Name")]
        [Required, StringLength(64, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual City Capital { get; set; }
    }
}
