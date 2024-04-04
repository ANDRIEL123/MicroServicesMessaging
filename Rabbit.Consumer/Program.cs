using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rabbit.Consumer;

try
{
    Console.WriteLine("Start Consumer...");

    var hostBuilder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<MailConsumer>();
        })
        .Build();

    hostBuilder.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting consumer: {ex.Message}");
}