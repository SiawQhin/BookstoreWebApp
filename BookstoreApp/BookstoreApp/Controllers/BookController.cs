using BookstoreApp.Data;
using BookstoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
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

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            if (userName != null)
            {
                ViewBag.UserId = userId;
                ViewBag.UserName = userName;
                var books = _booksQuery.GetAllBooks();
                ViewBag.Books = books;
                ViewBag.Bookings = _booksQuery.GetBookings();
                return View(books);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public IActionResult Index(string searchName)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            ViewBag.SearchName = searchName;
            ViewBag.UserId = userId;
            ViewBag.UserName = userName;
            ViewBag.Bookings = _booksQuery.GetBookings();
            var books = _booksQuery.GetBooks(searchName);
            return View(books);
        }
        public IActionResult Reserve(int? id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var userName = HttpContext.Session.GetString("UserName");
            var intUserId = Convert.ToInt16(userId);
            var book = _booksQuery.FindBook(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.UserId = intUserId;

            //check if the book is booked
            var checkBooking = _booksQuery.GetBookingByBookId(id);

            if (checkBooking == null)
            {
                //booking not found
                var newBooking = _booksCommand.ReserveBooking(id, intUserId);
                ViewBag.BookingNumber = newBooking.Id;
                ViewBag.IsBookSuccess = true;
                return View();
            };

            ViewBag.IsBookSuccess = false;
            return View();
        }

    }
}


