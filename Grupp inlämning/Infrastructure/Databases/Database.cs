using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }

    }
}
