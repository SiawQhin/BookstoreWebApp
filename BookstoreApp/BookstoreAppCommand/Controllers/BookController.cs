using BookstoreAppCommand.Data;
using BookstoreAppCommand.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using HttpMethod = System.Net.Http.HttpMethod;

namespace BookstoreAppCommand.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookCommand _booksCommand;
        private readonly IBookQuery _booksQuery;
        private readonly HttpClient _httpClient;
        

        public BookController(IBookCommand booksCommand, IBookQuery booksQuery, IHttpClientFactory httpClientFactory)
        {
            _booksCommand = booksCommand;
            _booksQuery = booksQuery;
            _httpClient = httpClientFactory.CreateClient("ReserveAPIClient");
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(int bookId, int userId)
        {
            var IsBookSuccess = 0;
            var checkBooking = _booksQuery.GetBookingByBookId(bookId);
           
            if (checkBooking == null)
            {
                //booking not found
                var newBooking = _booksCommand.ReserveBooking(bookId, userId);
                
                HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(newBooking), Encoding.UTF8, "application/json");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri: "https://localhost:7260/api/Reserve");
                requestMessage.Content = stringContent;
                var response = await _httpClient.SendAsync(requestMessage);

                //string apiResponse = response.Content.ReadAsStringAsync().Result;

                //var result = await response.Content.ReadAsStringAsync();
                IsBookSuccess = 1;
                return Redirect($"https://localhost:8011/Book/Reserve?bookingNumber={newBooking.Id}&isBookSuccess={IsBookSuccess}");
            };

            return Redirect($"https://localhost:8011/Book/Reserve?bookingNumber=&isBookSuccess={IsBookSuccess}");
        }

    }
}


