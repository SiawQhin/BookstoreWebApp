using BookstoreAppWorkerService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAppWorkerService.Data
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
