using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sep2026.NanHsuThetPaing.Battleship.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BattleshipGame> BattleshipGames { get; set; }

    public virtual DbSet<BattleshipShip> BattleshipShips { get; set; }

    public virtual DbSet<BattleshipShot> BattleshipShots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Sep2026Db;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BattleshipGame>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Battlesh__2AB897FD2044D34F");

            entity.ToTable("BattleshipGame");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<BattleshipShip>(entity =>
        {
            entity.HasKey(e => e.ShipId).HasName("PK__Battlesh__2A05CAB3B77FE6CF");

            entity.ToTable("BattleshipShip");

            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.Game).WithMany(p => p.BattleshipShips)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Battleshi__GameI__60A75C0F");
        });

        modelBuilder.Entity<BattleshipShot>(entity =>
        {
            entity.HasKey(e => e.ShotId).HasName("PK__Battlesh__66C5A78FDA45DA73");

            entity.ToTable("BattleshipShot");

            entity.Property(e => e.Result).HasMaxLength(10);

            entity.HasOne(d => d.Game).WithMany(p => p.BattleshipShots)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Battleshi__GameI__656C112C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
