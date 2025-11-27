using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TripsMicroservice.Entities;

namespace TripsMicroservice.Data;

public partial class TripsDbContext : DbContext
{
    public TripsDbContext()
    {
    }

    public TripsDbContext(DbContextOptions<TripsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripFare> TripFares { get; set; }

    public virtual DbSet<TripState> TripStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("PK__Trip__9B5492D197A919F9");

            entity.ToTable("Trip");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DestLat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.DestLng).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.DestinationAddress).HasMaxLength(200);
            entity.Property(e => e.OriginAddress).HasMaxLength(200);
            entity.Property(e => e.OriginLat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.OriginLng).HasColumnType("decimal(9, 6)");

            entity.HasOne(d => d.IdTripStatesNavigation).WithMany(p => p.Trips)
                .HasForeignKey(d => d.IdTripStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_States");
        });

        modelBuilder.Entity<TripFare>(entity =>
        {
            entity.HasKey(e => e.IdTripFare).HasName("PK__TripFare__D4DB08A355C6F0BC");

            entity.ToTable("TripFare");

            entity.Property(e => e.BaseFare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CalculatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DistanceFare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TimeFare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalFare).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTripNavigation).WithMany(p => p.TripFares)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripFare_Trip");
        });

        modelBuilder.Entity<TripState>(entity =>
        {
            entity.HasKey(e => e.IdTripStates).HasName("PK__TripStat__4989EACF0F989E9A");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
