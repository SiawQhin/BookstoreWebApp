using BookstoreAppCommand.Models;

namespace BookstoreAppCommand.Data
{
    public class DbBookCommandRepo : IBookCommand
    {
        private readonly CommandDatabaseContext _dbCommand;
        private readonly QueryDatabaseContext _dbQuery;
        public DbBookCommandRepo(CommandDatabaseContext dbCommand, QueryDatabaseContext dbQuery)
        {
            _dbCommand = dbCommand;
            _dbQuery = dbQuery;
        }

        public Booking? ReserveBooking(int? bookId, int? userId)
        {
            if (bookId == null || userId == null) //check if bookId or userId is null
            {
                return null;
            }
            var newBooking = _dbCommand.Bookings.Add(new Booking
            {
                BookId = (int)bookId,
                UserId = (int)userId,

            });
            _dbQuery.Bookings.Add(new Booking
            {
                BookId = (int)bookId,
                UserId = (int)userId,

            });
            _dbCommand.SaveChanges();
            _dbQuery.SaveChanges();
            return newBooking.Entity;
        }
    }
}
