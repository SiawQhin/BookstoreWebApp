using BookstoreAppQuery.Models;

namespace BookstoreAppQuery.Data
{
    public interface IBookCommand
    {
        public Booking? ReserveBooking(int? bookId, int? userId);
    }
}
