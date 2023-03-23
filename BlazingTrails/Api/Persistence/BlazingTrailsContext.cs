using BlazingTrails.Api.Persistence.Configurations;
using BlazingTrails.Api.Persistence.Entitities;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Persistence;

/*
 *  This BlazingTrailsContext class is a combination of Repository pattern and the Unit of Work pattern.
 */
public class BlazingTrailsContext : DbContext
{
    public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }

    /// <summary>
    /// This method is only called once when the first instance of a derived context is created.
    /// The model for that context is then cached and is for all further instances of the context in
    /// the app domain.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TrailConfig());
        modelBuilder.ApplyConfiguration(new RouteInstructionConfig());
    }

    //DbSet<T>(s) are essentially repository and allow us a way to interact with the tables
    //containing entity data in the database.
    
    public DbSet<Trail> Trails => Set<Trail>();
    public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();
}