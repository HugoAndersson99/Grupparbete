using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Databases
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }
        

    }
}
