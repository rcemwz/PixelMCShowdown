using Microsoft.EntityFrameworkCore;
using PixelMCShowdownAPI.Models;

namespace PixelMCShowdownAPI.Database
{
    public class PixelMCShowdownDBContext : DbContext 
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<BattleStat> BattleStats { get; set; }

        public PixelMCShowdownDBContext(DbContextOptions<PixelMCShowdownDBContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.UUID)
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasMany<BattleStat>(p => p.BattleStats)
                .WithMany(bs => bs.Players)
                .UsingEntity(j => j.ToTable("BattleStatsPlayers"));

            modelBuilder.Entity<BattleStat>()
                .HasMany<Player>(bs => bs.Winners)
                .WithMany(p => p.WonBattleStats)
                .UsingEntity(j => j.ToTable("BattleStatsWinners"));
        }
    }
}
