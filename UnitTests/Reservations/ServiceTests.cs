using AutoMapper;
using BusinessLogic.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace UnitTests.Reservations
{
    public class ServiceTests
    {
        private DenDbContext _dbContext;
        private IReservationService _service;
        private readonly IMapper _mapper = Mappings.CreateMapper();

        [SetUp]
        public void SetUp()
        {
            _dbContext = Initializer.DatabaseWithReservationSeeding();
            var repository = new ReservationRepository(_dbContext);
            _service = new ReservationService(repository, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
            _service = null;
        }

        [Test]
        public async Task GetUnpaidReservations()
        {
            int expectedCount = 3;

            var result = await _service.GetUnpaidReservations();

            Assert.That(result.Count, Is.EqualTo(expectedCount));
        }
    }
}
