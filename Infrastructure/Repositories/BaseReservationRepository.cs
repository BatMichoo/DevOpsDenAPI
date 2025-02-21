using BusinessLogic.Entities;
using BusinessLogic.Models.Reservations;
using BusinessLogic.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class BaseReservationRepository : IRepository<Reservation, ReservationCreateModel, ReservationEditModel>
    {
        private readonly DenDbContext _dbContext;

        public BaseReservationRepository(DenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Reservation> Create(ReservationCreateModel newModel)
        {
            var newEntity = new Reservation()
            {
                Price = newModel.Price,
                Adults = newModel.Adults,
                Children = newModel.Children,
                StartDate = newModel.StartDate,
                EndDate = newModel.EndDate,
                PansionId = newModel.PansionId,
            };

            _dbContext.Reservations.Add(newEntity);

            await _dbContext.SaveChangesAsync();

            return newEntity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == id);

            if (entity is not null)
            {
                _dbContext.Reservations.Remove(entity);

                int result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }

            return false;
        }        

        public async Task<List<Reservation>> GetAll()
        {
            var reservationList = await _dbContext.Reservations
                .Include(r => r.Pansion)                
                .ToListAsync();

            if (reservationList is null)
            {
                return new List<Reservation>();
            }

            return reservationList;
        }

        public async Task<Reservation?> GetById(int id)
        {
            var reservationEntity = await _dbContext.Reservations
                .Include(r => r.Pansion)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return null;
            }

            return reservationEntity;
        }

        public IQueryable<Reservation> GetQueryable()
            => _dbContext.Reservations.AsQueryable();

        public async Task<Reservation> Update(ReservationEditModel updateModel)
        {
            var existingEntity = await _dbContext.Reservations
                .Include(r => r.Pansion)
                .FirstOrDefaultAsync(r => r.Id == updateModel.Id);

            existingEntity.Adults = updateModel.Adults;
            existingEntity.Children = updateModel.Children;
            existingEntity.StartDate = updateModel.StartDate;
            existingEntity.EndDate = updateModel.EndDate;
            existingEntity.Price = updateModel.Price;
            existingEntity.PansionId = updateModel.PansionId;

            await _dbContext.SaveChangesAsync();

            return existingEntity;
        }
    }
}
