using DimDimApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DimDimApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Conta> Contas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contas)
            .WithOne(c => c.Cliente)
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
