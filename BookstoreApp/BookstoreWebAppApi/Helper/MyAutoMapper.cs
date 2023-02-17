using AutoMapper;
using BookstoreWebAppApi.Models;

namespace BookstoreWebAppApi.Helper
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            // Source to Destination
            CreateMap<ReserveDto, Booking>();
        }
    }
}
