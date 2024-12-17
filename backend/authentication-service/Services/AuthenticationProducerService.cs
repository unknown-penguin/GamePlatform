using Confluent.Kafka;

namespace AuthenticationService.Services
{
    public class AuthenticationProducerService
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<AuthenticationProducerService> _logger;

        public AuthenticationProducerService(IConfiguration configuration, ILogger<AuthenticationProducerService> logger)
        {
            _config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"], 
                SecurityProtocol = SecurityProtocol.Plaintext
            };

            _producer = new ProducerBuilder<string, string>(_config).Build();
            _logger = logger;
        }
        /*{
  "user_id": "user_001",
  "roles": ["user", "admin"],
  "permissions": ["create", "edit", "delete"],
  "mfa_status": "completed",
  "account_status": "active",
  "lock_status": "none",
  "last_login": "2024-12-14T12:34:56Z",
  "security_group": "finance",
  "data_consent": "true",
  "location": { "city": "New York", "country": "US" }
}*/
        public async Task ProduceMessageAsync(string topic, string message)// modify for session
        {
            try
            {
                _logger.LogInformation($"Producing message to topic: {topic}");
                var deliveryResult = await _producer.ProduceAsync(topic, new Message<string, string> { Key = message, Value = "message" });
                _logger.LogInformation($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");
            }
            catch (ProduceException<string, string> ex)
            {
                _logger.LogError($"Delivery failed: {ex.Error.Reason}");
            }
            finally
            {
                _producer.Flush(TimeSpan.FromSeconds(10));
                _logger.LogInformation("Producer flushed.");
            }
        }
    }
}