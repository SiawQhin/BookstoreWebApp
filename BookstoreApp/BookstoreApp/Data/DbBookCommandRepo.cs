using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    public class DbBookCommandRepo : IBookCommand
    {
        private readonly DatabaseContext _db;
        public DbBookCommandRepo(DatabaseContext db)
        {
            _db = db;
        }
        public Booking? ReserveBooking(int? bookId, int? userId)
        {
            if (bookId == null || userId == null) //check if bookId or userId is null
            {
                return null;
            }
            var newBooking = _db.Bookings.Add(new Booking
            {
                BookId = (int)bookId,
                UserId = (int)userId,

            });
            _db.SaveChanges();
            return newBooking.Entity;
        }
    }
}
