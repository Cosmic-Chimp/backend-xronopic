using Microsoft.EntityFrameworkCore;
using Xronopic.Api.Models;

namespace Xronopic.Api.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Timeline> Timelines => Set<Timeline>();
    public DbSet<Event> Events => Set<Event>();
  }
}