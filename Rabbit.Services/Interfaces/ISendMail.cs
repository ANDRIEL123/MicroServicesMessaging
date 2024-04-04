using Rabbit.Models.Entities;

namespace Rabbit.Services.Implementations
{
    public interface ISendMail
    {
        /// <summary>
        /// Envia a notificação para o cliente
        /// </summary>
        /// <param name="rabbitMessage"></param>
        /// <returns></returns>
        bool SendNotification(RabbitMessage rabbitMessage);
    }
}
