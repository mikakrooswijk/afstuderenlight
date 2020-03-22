using AL.Bestellingen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.WebshopService.DAL
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions options) : base(options) { }

        public DbSet<Bestelling> Bestellingen { get; set; }

    }
}
