using ICADProcessoSeletivo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ICADProcessoSeletivo.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Responsavel)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.ResponsavelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
