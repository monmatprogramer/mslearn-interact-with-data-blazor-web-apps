using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    public PizzaStoreContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PizzaSpecial> Specials { get; set; }
}
/*
    Step 1: Add a database context
    * creates a database context we can use to register a database service
    * also allows us to have a controller that accesses the database.

*/