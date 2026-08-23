using ClassLib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLib.Data.Contexts;

public class DatingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }

}