using Microsoft.EntityFrameworkCore;
using PersonasApp.Entities;
using PersonsApp.Entities;

namespace PersonsApp.Database
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
    }
}