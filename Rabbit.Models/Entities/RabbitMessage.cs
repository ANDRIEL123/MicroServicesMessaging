namespace Rabbit.Models.Entities
{
    public class RabbitMessage
    {
        public string Receiver { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
