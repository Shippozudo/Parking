using RabbitMQ.Client;
using System.Text;

namespace rabbitmq
{
    class Sender
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

            Console.WriteLine("Enviando mensagem .....");

            var mensagem = "Olá esta é uma mensagem para a fila02";
            var body = Encoding.UTF8.GetBytes(mensagem);

            channel.BasicPublish
            (
                exchange: "",
                routingKey: "fila02",
                basicProperties: null,
                body
            );

            Console.WriteLine("Mensagem enviada");
        }
    }
}
