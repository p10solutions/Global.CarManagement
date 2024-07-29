using Confluent.Kafka;
using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Domain.Models.Events.Cars;
using Microsoft.Extensions.Configuration;

namespace Global.CarManagement.Infraestructure.Events.Cars
{
    public class CarProducer : ICarProducer
    {
        readonly ProducerConfig _config;
        readonly string _topicCreated;
        readonly string _topicUpdated;
        readonly string _topicDeleted;

        public CarProducer(IConfiguration configuration)
        {
            _topicCreated = configuration.GetSection("Kafka:TopicCreated").Value;
            _topicUpdated = configuration.GetSection("Kafka:TopicUpdated").Value;
            _topicDeleted = configuration.GetSection("Kafka:TopicDeleted").Value;
            _config = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
            };
        }

        public async Task SendCreatedEventAsync(CreatedCarEvent @event)
        {
            using var producer = new ProducerBuilder<Null, CreatedCarEvent>(_config)
                .SetValueSerializer(new CreatedCarEventSerializer())
                .Build();

            var message = new Message<Null, CreatedCarEvent>() { Value = @event };

            await producer.ProduceAsync(_topicCreated, message);
        }

        public async Task SendDeletedEventAsync(DeletedCarEvent @event)
        {
            using var producer = new ProducerBuilder<Null, DeletedCarEvent>(_config)
                .SetValueSerializer(new DeletedCarEventSerializer())
                .Build();

            var message = new Message<Null, DeletedCarEvent>() { Value = @event };

            await producer.ProduceAsync(_topicDeleted, message);
        }

        public async Task SendUpdatedEventAsync(UpdatedCarEvent @event)
        {
            using var producer = new ProducerBuilder<Null, UpdatedCarEvent>(_config)
                .SetValueSerializer(new UpdatedCarEventSerializer())
                .Build();

            var message = new Message<Null, UpdatedCarEvent>() { Value = @event };

            await producer.ProduceAsync(_topicUpdated, message);
        }
    }
}
