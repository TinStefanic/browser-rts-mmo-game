using BrowserGame.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrowserGame.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Clay> Clays { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Iron> Irons { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ResourceField> ResourceFields { get; set; }
        public DbSet<UpgradeInfo> UpgradeInfos { get; set; }
        public DbSet<Wood> Woods { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}