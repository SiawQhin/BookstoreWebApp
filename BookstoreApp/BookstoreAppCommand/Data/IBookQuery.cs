using BookstoreAppCommand.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAppCommand.Data
{
    public interface IBookQuery
    {
        public Book[] GetAllBooks();
        public Book[] GetBooks(string searchName);
        public Book? FindBook(int? id);
        public Booking[] GetBookings();
        public Booking? GetBookingByBookId(int? bookId);
    }
}
