using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Rabbit.Models.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Rabbit.Consumer.Base
{
    public abstract class BaseConsumer : BackgroundService
    {
        private readonly string _queueName;

        public BaseConsumer(string queueName)
        {
            _queueName = queueName;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() => StartConsumer(stoppingToken), stoppingToken);
            }
        }

        public void StartConsumer(CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                Console.WriteLine("Waiting for messages");
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var stringMessage = Encoding.UTF8.GetString(body);

                    var rabbitModel = JsonConvert.DeserializeObject<RabbitMessage>(stringMessage);

                    Console.WriteLine($"Receive message: {stringMessage}");

                    if (rabbitModel != null)
                        StartTask(rabbitModel);

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer
                );

                cancellationToken.WaitHandle.WaitOne();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting consumer: {ex.Message}");
            }
        }

        public virtual void StartTask(RabbitMessage rabbitMessage) { }
    }
}
