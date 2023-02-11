using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    public interface IBookCommand
    {
        public Booking? ReserveBooking(int? bookId, int? userId);
    }
}
