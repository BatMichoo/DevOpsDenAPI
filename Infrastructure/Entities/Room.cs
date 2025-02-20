using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Room
{
    public int Id { get; set; }

    public int TypeId { get; set; }

    public int Floor { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual RoomType Type { get; set; } = null!;
}
