using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Payment
{
    public int Id { get; set; }

    public DateTime CompletedAt { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
