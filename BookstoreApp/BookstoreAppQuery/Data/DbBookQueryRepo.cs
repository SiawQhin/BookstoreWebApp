using BookstoreAppQuery.Models;

namespace BookstoreAppQuery.Data
{
    public class DbBookQueryRepo : IBookQuery
    {
        private readonly QueryDatabaseContext _db;

        public DbBookQueryRepo(QueryDatabaseContext db)
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
