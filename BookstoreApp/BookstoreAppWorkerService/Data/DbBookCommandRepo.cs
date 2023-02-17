using BookstoreAppWorkerService.Models;
using BookstoreAppWorkerService.Models.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BookstoreAppWorkerService.Data
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

        public void PublishReserveBookingMessage(int bookId, int userId)
        {
            string _publishKey = "reserve_book";
            var _factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            var _connection = _factory.CreateConnection();
            using var _channel = _connection.CreateModel();
            _channel.QueueDeclare(_publishKey, exclusive: false);

            ReservedBookEvent _event = new()
            {
                BookId = bookId,
                UserId = userId,
            };
            MyEvent myEvent = new()
            {
                EventName = "reservebook",
                Payload = _event
            };
            var json = JsonConvert.SerializeObject(myEvent);

            var _body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: _publishKey, body: _body);
        }
    
    }
}
