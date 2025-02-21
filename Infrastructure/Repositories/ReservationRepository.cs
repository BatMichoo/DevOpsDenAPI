using BusinessLogic.Entities;
using BusinessLogic.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : BaseReservationRepository, IReservationRepository
    {
        public ReservationRepository(DenDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Reservation>> ExecuteQuery(IQueryable<Reservation> query)
        {
            var result = await query
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}
