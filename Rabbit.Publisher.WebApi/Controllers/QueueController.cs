using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rabbit.Models.Entities;
using Rabbit.Publishers.Base;
using System.Text;

[ApiController]
public class QueueController : ControllerBase
{
    [HttpPost]
    [Route("SendQueue")]
    public IActionResult Billing()
    {
        var rabbitMessage = JsonConvert.SerializeObject(new RabbitMessage
        {
           Receiver = "andrielmfriedrich@gmail.com",
           Description = "xxxxxxxxxxxxxxxxxxxxx",
           Title = "xxxxxxxxxxxxxxxxxxxx"
        });

        PublisherBase.Send(Encoding.UTF8.GetBytes(rabbitMessage), "Queue.Mail");

        return Ok("Enviado para a fila de processamento.");
    }
}