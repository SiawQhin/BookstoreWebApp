using BookstoreApp.Models;

namespace BookstoreApp.Data
{
    public class DbBookQueryRepo : IBookQuery
    {
        private readonly DatabaseContext _db;

        public DbBookQueryRepo(DatabaseContext db)
        {
            _db = db;
        }

        public Book[] GetAllBooks()
        {
            return _db.Books.ToArray();
        }

        public Book[] GetBooks(string searchName)
        {
            if ((searchName != null) && (searchName.Length > 0))
            {
                return _db.Books.Where(b => b.Title.Contains(searchName)).ToArray();
            }
            return _db.Books.ToArray();
        }
        public Book? FindBook(int? id)
        {
            return _db.Books.FirstOrDefault(b => b.Id == id);
        }

        public Booking[] GetBookings()
        {
            return _db.Bookings.ToArray();
        }

        public Booking? GetBookingByBookId(int? bookId)
        {
            return GetBookings().FirstOrDefault(b => b.BookId == bookId);
        }
    }
}
