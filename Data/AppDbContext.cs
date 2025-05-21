using Microsoft.EntityFrameworkCore;
using backend_jwt_auth.Models;

namespace backend_jwt_auth.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
}
