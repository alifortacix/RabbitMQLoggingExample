using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Entities;
using System.Text;

namespace RabbitMQ.Services
{
    public class RabbitMQService
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMQService(IConfiguration configuration)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
        }


        public void SendLog(Log log)
        {
            channel.QueueDeclare(queue: "logs", durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.ExchangeDeclare("exchange_log_t", "topic");
            channel.QueueBind("logs", "exchange_log_t", "logs");

            var msg = JsonConvert.SerializeObject(log);
            var body = Encoding.UTF8.GetBytes(msg);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true; // Mesajların kalıcı olmasını sağlar.
            channel.BasicPublish(exchange: "exchange_log_t", routingKey: "logs", basicProperties: properties, body: body);
            Console.WriteLine($"{msg} sended.");
        }
    }
}
