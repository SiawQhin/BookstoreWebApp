using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BookstoreAppWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string _publishKey = "reserve_book";
                var _factory = new ConnectionFactory
                {
                    HostName = "localhost",
                };

                var _connection = _factory.CreateConnection();
                using var _channel = _connection.CreateModel();
                _channel.QueueDeclare(_publishKey, exclusive: false);

                var _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += (model, eventArgs) =>
                {
                    var _body = eventArgs.Body.ToArray();
                    var _message = Encoding.UTF8.GetString(_body);
                    Console.WriteLine($"Product Message Received : {_message}");
                };

                _channel.BasicConsume(queue: "reserve_book", autoAck: true, consumer: _consumer);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}