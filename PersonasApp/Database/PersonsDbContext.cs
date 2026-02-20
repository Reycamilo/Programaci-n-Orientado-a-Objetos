using Microsoft.EntityFrameworkCore;
using PersonApp.Entities;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<PersonEntity> Persons { get; set; }
}