namespace BusinessLogic.Models.Reservations
{
    public class ReservationCreateModel
    {
        public decimal Price { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PansionId { get; set; }
    }
}
