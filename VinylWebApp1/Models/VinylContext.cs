using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class VinylContext : DbContext
    {
        public VinylContext(DbContextOptions<VinylContext> options) : base(options)
        {
        }

        public DbSet<Vinyl> Vinyls { get; set; }
        public DbSet<VinylReservation> Reservations { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
