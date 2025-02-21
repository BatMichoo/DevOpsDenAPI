using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Pansion
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
