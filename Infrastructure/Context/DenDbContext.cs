using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class DenDbContext : DbContext
{
    public DenDbContext()
    {
    }

    public DenDbContext(DbContextOptions<DenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Pansion> Pansions { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC07BAB754F0");

            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);

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

        modelBuilder.Entity<Pansion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pansions__3214EC0775899A74");

            entity.Property(e => e.Type).HasMaxLength(15);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC0794D6DB88");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.PansionId).HasDefaultValueSql("((1))");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Pansion).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.PansionId)
                .HasConstraintName("FK__Reservati__Pansi__412EB0B6");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Reservati__RoomI__403A8C7D");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC07F7A5D93F");

            entity.Property(e => e.Number).HasComputedColumnSql("([Floor]*(100)+[Id])", true);

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
            entity.Property(e => e.TotalBeds).HasComputedColumnSql("([AdultBeds]+isnull([ChildBeds],(0)))", true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
