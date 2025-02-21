using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.MapperProfiles;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public static class Initializer
    {
        public static DenDbContext Database()
        {
            var options = new DbContextOptionsBuilder<DenDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            return new DenDbContext(options);
        }

        public static DenDbContext DatabaseWithReservationSeeding()
        {
            var context = Database();

            context.WithReservations();

            return context;
        }
    }
    public static class Seeder
    {
        public static DenDbContext WithReservations(this DenDbContext context)
        {
            context.WithPansions();

            var reservationList = new List<Reservation>()
            {
                new Reservation
                {
                    Price = 1000m,
                    Adults = 2,
                    StartDate = DateTime.Parse("20.2.2025"),
                    EndDate = DateTime.Parse("28.2.2025"),
                    PansionId = 1
                },
                new Reservation
                {
                    Price = 2000m,
                    Adults = 2,
                    Children = 2,
                    StartDate = DateTime.Parse("28.2.2025"),
                    EndDate = DateTime.Parse("08.3.2025"),
                    PansionId = 2
                },
                new Reservation
                {
                    Price = 3000m,
                    Adults = 2,
                    Children = 3,
                    StartDate = DateTime.Parse("18.3.2025"),
                    EndDate = DateTime.Parse("28.3.2025"),
                    PansionId = 3
                }
            };

            context.AddRange(reservationList);

            context.SaveChanges();

            return context;
        }

        public static DenDbContext WithPansions(this DenDbContext context)
        {
            var pansionList = new List<Pansion>()
            {
                new Pansion {Type = "Breakfast"},
                new Pansion {Type = "Half"},
                new Pansion {Type = "Full"},
            };

            context.Pansions.AddRange(pansionList);

            context.SaveChanges();

            return context;
        }
    }

    public static class Mappings
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReservationProfile>();
            });

            return config.CreateMapper();
        }
    }
}
