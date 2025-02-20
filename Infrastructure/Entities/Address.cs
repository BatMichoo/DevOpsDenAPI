using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Address
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public string? PostalCode { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
