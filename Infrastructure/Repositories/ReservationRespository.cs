using BusinessLogic.Models;
using BusinessLogic.Models.Reservations;
using BusinessLogic.Repositories;
using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRespository : IRepository<ReservationModel, ReservationCreateModel, ReservationEditModel>
    {
        private readonly DenDbContext _dbContext;

        public ReservationRespository(DenDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReservationModel> Create(ReservationCreateModel newModel)
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

            var createdModel = new ReservationModel()
            {
                Id = newEntity.Id,
                Price = newEntity.Price,
                Adults = newEntity.Adults,
                Children = newEntity.Children,
                StartDate = newEntity.StartDate,
                EndDate = newEntity.EndDate,
                Pansion = new PansionType()
                {
                    Id = newEntity.Pansion.Id,
                    Type = newEntity.Pansion.Type
                },
            };

            return createdModel;
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

        public async Task<List<ReservationModel>> GetAll()
        {
            var reservationList = await _dbContext.Reservations
                .Include(r => r.Pansion)
                .Select(r => new ReservationModel
                {
                    Id = r.Id,
                    Price = r.Price,
                    Adults = r.Adults,
                    Children = r.Children,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    GuestCount = r.GuestCount,
                    CreatedAt = r.CreatedAt,
                    Pansion = new PansionType
                    {
                        Id = r.Pansion.Id,
                        Type = r.Pansion.Type,
                    }
                })
                .ToListAsync();

            if (reservationList is null)
            {
                return new List<ReservationModel>();
            }

            return reservationList;
        }

        public async Task<ReservationModel?> GetById(int id)
        {
            var reservationEntity = await _dbContext.Reservations
                .Include(r => r.Pansion)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservationEntity is null)
            {
                return null;
            }

            var pansion = new PansionType()
            {
                Id = reservationEntity.PansionId,
                Type = reservationEntity.Pansion.Type
            };

            //RoomModel? room = reservationEntity.Room is not null ? new RoomModel()
            //{
            //    Id = (int)reservationEntity.RoomId,
            //    Number = reservationEntity.Room.Number
            //}
            //: null;

            return new ReservationModel()
            {
                Id = reservationEntity.Id,
                CreatedAt = reservationEntity.CreatedAt,
                Adults = reservationEntity.Adults,
                Children = reservationEntity.Children,
                StartDate = reservationEntity.StartDate,
                EndDate = reservationEntity.EndDate,
                Pansion = pansion,
                GuestCount = reservationEntity.GuestCount,
                Price = reservationEntity.Price,
            };
        }

        public async Task<ReservationModel> Update(ReservationEditModel updateModel)
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

            var pansion = new PansionType()
            {
                Id = existingEntity.Pansion.Id,
                Type = existingEntity.Pansion.Type,
            };

            var updatedModel = new ReservationModel()
            {
                Id = existingEntity.Id,
                CreatedAt = existingEntity.CreatedAt,
                Adults = existingEntity.Adults,
                Children = existingEntity.Children,
                StartDate = existingEntity.StartDate,
                EndDate = existingEntity.EndDate,
                Pansion = pansion,
                GuestCount = existingEntity.GuestCount,
                Price = existingEntity.Price,
            };

            return updatedModel;
        }
    }
}
