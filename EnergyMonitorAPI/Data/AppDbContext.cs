using EnergyMonitorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyMonitorAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();
    public DbSet<LeituraConsumo> Leituras => Set<LeituraConsumo>();
    public DbSet<Alerta> Alertas => Set<Alerta>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeituraConsumo>()
            .HasOne(l => l.Equipamento)
            .WithMany(e => e.Leituras)
            .HasForeignKey(l => l.EquipamentoId);

        modelBuilder.Entity<Alerta>()
            .HasOne(a => a.Equipamento)
            .WithMany(e => e.Alertas)
            .HasForeignKey(a => a.EquipamentoId);
    }
}