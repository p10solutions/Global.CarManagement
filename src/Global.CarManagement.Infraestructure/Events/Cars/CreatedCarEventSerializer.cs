using Confluent.Kafka;
using Global.CarManagement.Domain.Models.Events.Cars;
using System.Text;
using System.Text.Json;

namespace Global.CarManagement.Infraestructure.Events.Cars
{
    public class CreatedCarEventSerializer : IAsyncSerializer<CreatedCarEvent>
    {
        public Task<byte[]> SerializeAsync(CreatedCarEvent data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Task.FromResult(Encoding.ASCII.GetBytes(json));
        }
    }
}
