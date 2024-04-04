using Microsoft.Extensions.DependencyInjection;
using Rabbit.Consumer.Base;
using Rabbit.Models.Entities;
using Rabbit.Services.Implementations;

namespace Rabbit.Consumer
{
    public class MailConsumer : BaseConsumer
    {
        public MailConsumer() : base("Queue.Mail") { }

        public override void StartTask(RabbitMessage rabbitMessage)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ISendMail, SendMail>()
                .BuildServiceProvider();

            serviceProvider.GetRequiredService<ISendMail>().SendNotification(rabbitMessage);
        }
    }
}
