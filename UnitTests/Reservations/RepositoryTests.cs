using BusinessLogic.Models.Reservations;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace UnitTests.Reservations
{
    public class RepositoryTests
    {
        private DenDbContext _context;
        private ReservationRespository? _repository;


        [SetUp]
        public void Setup()
        {
            _context = Initializer.DatabaseWithReservationSeeding();

            _repository = new ReservationRespository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _repository = null;
        }

        [Test]
        public async Task A_Test_Get_Returns_Null()
        {
            int idToSeachFor = 15;

            var result = await _repository!.GetById(idToSeachFor);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task A_Test_Get_Returns_ReservationId1()
        {
            var expected = new Reservation
            {
                Id = 1,
                Price = 1000m,
                Adults = 2,
                StartDate = DateTime.Parse("20.2.2025"),
                EndDate = DateTime.Parse("28.2.2025"),
                PansionId = 1
            };

            var result = await _repository!.GetById(expected.Id);

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Price, Is.EqualTo(expected.Price));
            Assert.That(result.Adults, Is.EqualTo(expected.Adults));
            Assert.That(result.Children, Is.EqualTo(expected.Children));
            Assert.That(result.Pansion.Id, Is.EqualTo(expected.PansionId));
            Assert.That(result.StartDate, Is.EqualTo(expected.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(expected.EndDate));
        }

        [Test]
        public async Task A_GetAll_Returns_ThreeItems()
        {
            var result = await _repository!.GetAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task B_Create_Creates()
        {
            var expected = new Reservation
            {
                Id = 4,
                Price = 5000m,
                Adults = 2,
                Children = 1,
                StartDate = DateTime.Parse("30.3.2025"),
                EndDate = DateTime.Parse("05.4.2025"),
                PansionId = 3
            };

            var toBeCreated = new ReservationCreateModel()
            {
                Price = 5000m,
                Adults = 2,
                Children = 1,
                StartDate = DateTime.Parse("30.3.2025"),
                EndDate = DateTime.Parse("05.4.2025"),
                PansionId = 3
            };

            var result = await _repository!.Create(toBeCreated);

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Price, Is.EqualTo(expected.Price));
            Assert.That(result.Adults, Is.EqualTo(expected.Adults));
            Assert.That(result.Children, Is.EqualTo(expected.Children));
            Assert.That(result.Pansion.Id, Is.EqualTo(expected.PansionId));
            Assert.That(result.StartDate, Is.EqualTo(expected.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(expected.EndDate));
        }

        [Test]
        public async Task Delete_NonExistent_Returns_False()
        {
            var idToDelete = 15;

            bool result = await _repository!.Delete(idToDelete);

            Assert.That(result, Is.False);
        }


        [Test]
        public async Task Delete_Exists_Returns_True()
        {
            var idToDelete = 1;

            bool result = await _repository!.Delete(idToDelete);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task C_Update()
        {
            var expected = new Reservation
            {
                Id = 2,
                Price = 5000m,
                Adults = 2,
                Children = 1,
                StartDate = DateTime.Parse("30.3.2025"),
                EndDate = DateTime.Parse("05.4.2025"),
                PansionId = 3
            };

            var toBeUpdated = new ReservationEditModel()
            {
                Id = 2,
                Price = 5000m,
                Adults = 2,
                Children = 1,
                StartDate = DateTime.Parse("30.3.2025"),
                EndDate = DateTime.Parse("05.4.2025"),
                PansionId = 3
            };

            var result = await _repository!.Update(toBeUpdated);

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.Price, Is.EqualTo(expected.Price));
            Assert.That(result.Adults, Is.EqualTo(expected.Adults));
            Assert.That(result.Children, Is.EqualTo(expected.Children));
            Assert.That(result.Pansion.Id, Is.EqualTo(expected.PansionId));
            Assert.That(result.StartDate, Is.EqualTo(expected.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(expected.EndDate));
        }
    }
}
