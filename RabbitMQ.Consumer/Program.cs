using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Entities;
using System.Text;

Console.WriteLine("Rabbit MQ Consumer Started!");

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(queue: "logs", durable: false, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    Log message = JsonConvert.DeserializeObject<Log>(Encoding.UTF8.GetString(body));
    using (StreamWriter sw = File.AppendText("logs.txt"))
    {
        sw.WriteLine($"{message.LogType}: {message.Message}");
        Console.WriteLine($"{message.LogType}: {message.Message}");
    }
};

channel.BasicConsume(queue: "logs", autoAck: true, consumer: consumer);


Console.ReadLine();
