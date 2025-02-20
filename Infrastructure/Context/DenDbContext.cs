using System;
using System.Collections.Generic;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DenDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DenDbContext(DbContextOptions<DenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Pansion> Pansions { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC070EA854B9");

            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.Street).HasMaxLength(100);

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Addresses__CityI__59063A47");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC07B83CDFED");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__CountryI__5629CD9C");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07BAB754F0");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);

            entity.HasOne(d => d.Address).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clients__Address__5CD6CB2B");

            entity.HasMany(d => d.Reservations).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientsReservation",
                    r => r.HasOne<Reservation>().WithMany()
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK__ClientsRe__Reser__46E78A0C"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__ClientsRe__Clien__45F365D3"),
                    j =>
                    {
                        j.HasKey("ClientId", "ReservationId").HasName("PK__ClientsR__BD00FFD6FBFB34D8");
                        j.ToTable("ClientsReservations");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07FE5A0FF9");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Pansion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pansions__3214EC0775899A74");

            entity.Property(e => e.Type).HasMaxLength(15);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07CBE40F23");

            entity.Property(e => e.CompletedAt).HasPrecision(0);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC0794D6DB88");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.GuestCount).HasComputedColumnSql("([Adults]+[Children])", true);
            entity.Property(e => e.PansionId).HasDefaultValueSql("((1))");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Pansion).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PansionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Pansi__6FE99F9F");

            entity.HasOne(d => d.Payment).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Reservati__Payme__5DCAEF64");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Reservati__RoomI__403A8C7D");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC07F7A5D93F");

            entity.HasOne(d => d.Type).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__TypeId__398D8EEE");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomType__3214EC076F438460");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.TotalBeds).HasComputedColumnSql("([AdultBeds]+[ChildBeds])", true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
