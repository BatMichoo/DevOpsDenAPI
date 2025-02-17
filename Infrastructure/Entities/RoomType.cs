namespace Infrastructure.Entities;

public partial class RoomType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public int AdultBeds { get; set; }

    public int? ChildBeds { get; set; }

    public int? TotalBeds { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
