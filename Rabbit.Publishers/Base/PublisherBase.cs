using RabbitMQ.Client;

namespace Rabbit.Publishers.Base
{
    public class PublisherBase
    {
        public static void Send(byte[] body, string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body
            );
        }
    }
}
