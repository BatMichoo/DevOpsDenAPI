using AutoMapper;
using BusinessLogic.Models.Reservations;
using BusinessLogic.Repositories;

namespace BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<List<ReservationModel>> GetClientReservations(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationModel>> GetConfirmedReservations()
        {
            throw new NotImplementedException();
        }

        public Task<List<ReservationModel>> GetConfirmedReservations(DateOnly startDate, DateOnly endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReservationModel>> GetUnpaidReservations()
        {
            var query = _repository.GetQueryable()
                .BuildUnpaidReservations();

            var results = await _repository.ExecuteQuery(query);

            var modelList = _mapper.Map<List<ReservationModel>>(results);

            return modelList;
        }
    }
}
