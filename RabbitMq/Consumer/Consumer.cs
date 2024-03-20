using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare
            (
                queue: "fila02",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            Console.WriteLine("Esperando a mensagem");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, EventArgs) =>
            {
                var body = EventArgs.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);
                Console.WriteLine("Mensagem Recebida {0}", mensagem);
            };

            channel.BasicConsume
            (
                queue: "fila02",
                autoAck: true,
                consumer: consumer
            );

        }
    }
}
