using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CV> CVs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationen mellan User och CV
            modelBuilder.Entity<CV>()
                .HasOne(cv => cv.User)
                .WithMany(user => user.CVs)
                .HasForeignKey(cv => cv.UserId);
        }
    }
}
