using Bogus;
using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var faker = new Faker();
            using var p = new ProducerBuilder<int, string>(new ProducerConfig { BootstrapServers = "localhost:29092" }).Build();

            while (true)
            {
                Console.WriteLine("Click any key to generate a random value.");
                Console.ReadKey();

                try
                {                    
                    var dr = await p.ProduceAsync("cabeloTopic", new Message<int, string> { Key = 1, Value = faker.Company.Bs() });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
