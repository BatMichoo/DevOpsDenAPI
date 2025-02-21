using BusinessLogic.Models.Reservations;

namespace BusinessLogic.Services
{
    public interface IReservationService
    {
        Task<List<ReservationModel>> GetUnpaidReservations();
        Task<List<ReservationModel>> GetClientReservations(int clientId);
        Task<List<ReservationModel>> GetConfirmedReservations();
        Task<List<ReservationModel>> GetConfirmedReservations(DateOnly startDate, DateOnly endDate);
    }
}
