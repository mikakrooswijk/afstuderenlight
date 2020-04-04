using AL.WebshopService.Models;
using Microsoft.EntityFrameworkCore;

namespace AL.WebshopService.DAL
{
    public class WebshopContext: DbContext
    {
        public WebshopContext(DbContextOptions options) : base(options) { }

        public DbSet<Bestelling> Bestellingen { get; set; }

    }
}
