using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AuthenticationService.Services
{
    public class AuthenticationConsumerService : IHostedService
    {
        private readonly ILogger<AuthenticationConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly ConsumerConfig _config;
        private Task _executingTask = Task.CompletedTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public AuthenticationConsumerService(ILogger<AuthenticationConsumerService> logger)
        {
            _logger = logger;

            _config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "consumer1",
                GroupId = "consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true, // Handle Partition EOF
                EnableAutoCommit = true
            };

            _consumer = new ConsumerBuilder<string, string>(_config).Build();

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Kafka consumer...");
            _consumer.Subscribe("AuthenticatioTestTopicnTopic"); 
            _executingTask = Task.Run(() => ExecuteAsync(_stoppingCts.Token), cancellationToken);
            return Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        // Consume messages from all partitions
                        var consumeResult = _consumer.Consume(stoppingToken);

                        if (consumeResult.IsPartitionEOF)
                        {
                            _logger.LogInformation($"Reached end of partition {consumeResult.Partition} at offset {consumeResult.Offset}");
                            continue;
                        }

                        // Process the message
                        _logger.LogInformation($"Message received at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Key} - {consumeResult.Message.Value}");

                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError($"Error consuming message: {ex.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer execution canceled.");
            }
            finally
            {
                _consumer.Close();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Kafka consumer...");
            _stoppingCts.Cancel();
            return Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
}
