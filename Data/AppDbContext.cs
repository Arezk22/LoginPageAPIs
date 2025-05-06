using Microsoft.EntityFrameworkCore;

namespace LoginPageAPIs.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<LoginRequest> LoginRequests { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // العلاقة بين Report و LoginRequest
        modelBuilder.Entity<Report>()
            .HasOne(r => r.Child)
            .WithMany() // لو مش عايز ترجع قائمة من التقارير عند الطفل
            .HasForeignKey("ChildId");
    }
}