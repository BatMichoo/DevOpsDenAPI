using BusinessLogic.Entities;

namespace BusinessLogic
{
    public static class ReservationQueryBuilder
    {
        public static IQueryable<Reservation> BuildClientReservations(this IQueryable<Reservation> query, int clientId)
        {
            throw new NotImplementedException();
        }

        //public Task<IQueryable<Reservation>> BuildConfirmedReservations()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IQueryable<Reservation>> BuildConfirmedReservations(DateOnly startDate, DateOnly endDate)
        //{
        //    throw new NotImplementedException();
        //}

        public static IQueryable<Reservation> BuildUnpaidReservations(this IQueryable<Reservation> query)
            => query.Where(r => r.PaymentId == null);
    }
}
