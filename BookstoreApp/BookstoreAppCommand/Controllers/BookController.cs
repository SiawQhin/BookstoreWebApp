using BookstoreAppCommand.Data;
using BookstoreAppCommand.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreAppCommand.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookCommand _booksCommand;
        private readonly IBookQuery _booksQuery;

        public BookController(IBookCommand booksCommand, IBookQuery booksQuery)
        {
            _booksCommand = booksCommand;
            _booksQuery = booksQuery;
        }

        [HttpPost]
        public IActionResult Reserve(int id, short userId)
        {
            //var IsBookSuccess = 0;
            //var book = _booksQuery.FindBook(id);
            //if (book == null)
            //{
            //    return NotFound();
            //}

            ////check if the book is booked
            //var checkBooking = _booksQuery.GetBookingByBookId(id);

            //if (checkBooking == null)
            //{
            //    //booking not found
            //    var newBooking = _booksCommand.ReserveBooking(id, userId);
            //    IsBookSuccess = 1;
            //    return Redirect($"https://localhost:8011/Book/Pending");
            //    //return Redirect($"https://localhost:8011/Book/Reserve?bookingNumber={newBooking.Id}&isBookSuccess={IsBookSuccess}");
            //};
            _booksCommand.PublishReserveBookingMessage(id, userId);
            return Redirect($"https://localhost:8011/Book/Pending");
        }

       
    }
}


