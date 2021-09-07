using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace kafkaDemo
{
    public class Producer : IHostedService
    {


        private readonly IProducer<Null, string> _producer;
        public Producer()
        {

            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync("test", new Message<Null, string>()
            {
                Value = "this is kafka test"
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }

    }
}
