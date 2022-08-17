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

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<BattleStatPlayer>()
                .HasKey(bsp => new { bsp.PlayerId, bsp.BattleStatId });
            
            modelBuilder.Entity<BattleStatPlayer>()
                .HasOne(bsp => bsp.Player)
                .WithMany(player => player.BattleStatPlayers)
                .HasForeignKey(bsp => bsp.BattleStatId);

            modelBuilder.Entity<BattleStatPlayer>()
                .HasOne(bsp => bsp.BattleStat)
                .WithMany(bs => bs.BattleStatPlayers)
                .HasForeignKey(bsp => bsp.BattleStatId);


            modelBuilder.Entity<BattleStatWinner>()
                .HasKey(bsp => new { bsp.PlayerId, bsp.BattleStatId });

            modelBuilder.Entity<BattleStatWinner>()
                .HasOne(bsp => bsp.Winner)
                .WithMany(player => player.BattleStatWinners)
                .HasForeignKey(bsp => bsp.BattleStatId);

            modelBuilder.Entity<BattleStatWinner>()
                .HasOne(bsp => bsp.BattleStat)
                .WithMany(bs => bs.BattleStatWinners)
                .HasForeignKey(bsp => bsp.BattleStatId);

            modelBuilder.Entity<BattleStat>()
                .Navigation(bs => bs.Players)
                .UsePropertyAccessMode(PropertyAccessMode.)
            */

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
