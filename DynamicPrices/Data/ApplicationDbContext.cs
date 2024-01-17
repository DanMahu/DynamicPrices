using DynamicPrices.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicPricing.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Clienti> clienti { get; set; }
        public DbSet<Produse_Electronice> produse_electronice { get; set; }
        public DbSet<Preturi_Electronice> preturi_electronice { get; set; }
        public DbSet<Istoric_Preturi_Electronice> istoric_preturi_electronice { get; set; }
        public DbSet<Stoc_Electronice> stoc_electronice { get; set; }
        public DbSet<Vanzari_Electronice> vanzari_electronice { get; set; }
    }
}
