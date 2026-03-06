using Microsoft.EntityFrameworkCore;
using PersonsApp.Entities;

namespace PersonsApp.Database
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<PersonEntity> Persons { get; set; }
    }
}