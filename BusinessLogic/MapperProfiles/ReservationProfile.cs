using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.Models.Reservations;

namespace BusinessLogic.MapperProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationModel>();
        }
    }
}
