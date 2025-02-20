namespace BusinessLogic.Models.Reservations
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int Adults { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Children { get; set; }

        public int GuestCount { get; set; }

        public PansionType Pansion { get; set; } = null!;

        public PaymentModel? Payment { get; set; }

        public RoomModel? Room { get; set; }

        public ICollection<ClientModel> Clients { get; set; } = new List<ClientModel>();
    }
}
