using BookstoreAppCommand.Models;

namespace BookstoreAppCommand.Data
{
    public interface IBookCommand
    {
        public Booking? ReserveBooking(int? bookId, int? userId);
        public void PublishReserveBookingMessage(int bookId, int userId);
    }
}
