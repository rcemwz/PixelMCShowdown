// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PixelMCShowdownAPI.Database;

#nullable disable

namespace PixelMCShowdownAPI.Migrations
{
    [DbContext(typeof(PixelMCShowdownDBContext))]
    [Migration("20220911105705_Initial Create")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BattleStatPlayer", b =>
                {
                    b.Property<int>("BattleStatsId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("BattleStatsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("BattleStatsPlayers", (string)null);
                });

            modelBuilder.Entity("BattleStatPlayer1", b =>
                {
                    b.Property<int>("WinnersId")
                        .HasColumnType("int");

                    b.Property<int>("WonBattleStatsId")
                        .HasColumnType("int");

                    b.HasKey("WinnersId", "WonBattleStatsId");

                    b.HasIndex("WonBattleStatsId");

                    b.ToTable("BattleStatsWinners", (string)null);
                });

            modelBuilder.Entity("PixelMCShowdownAPI.Models.BattleStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("BattleStats");
                });

            modelBuilder.Entity("PixelMCShowdownAPI.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ELORating")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UUID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UUID")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BattleStatPlayer", b =>
                {
                    b.HasOne("PixelMCShowdownAPI.Models.BattleStat", null)
                        .WithMany()
                        .HasForeignKey("BattleStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PixelMCShowdownAPI.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BattleStatPlayer1", b =>
                {
                    b.HasOne("PixelMCShowdownAPI.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("WinnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PixelMCShowdownAPI.Models.BattleStat", null)
                        .WithMany()
                        .HasForeignKey("WonBattleStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
