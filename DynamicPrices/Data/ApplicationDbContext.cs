using DynamicPrices.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicPricing.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Produse_Electronice> produse_electronice { get; set; }
        public DbSet<Preturi_Electronice> preturi_electronice { get; set; }
        public DbSet<Istoric_Preturi_Electronice> istoric_preturi_electronice { get; set; }
    }
}
