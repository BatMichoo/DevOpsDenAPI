﻿namespace Infrastructure.Entities;

public partial class Reservation
{
    public int Id { get; set; }

    public int? RoomId { get; set; }

    public decimal Price { get; set; }

    public int Adults { get; set; }

    public int? Children { get; set; }

    public int? PansionId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? PaymentId { get; set; }

    public virtual Pansion? Pansion { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Room? Room { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
