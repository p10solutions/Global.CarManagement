using Confluent.Kafka;
using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Domain.Models.Events.Cars;
using System.Text;
using System.Text.Json;

namespace Global.CarManagement.Infraestructure.Events.Cars
{
    public class DeletedCarEventSerializer : IAsyncSerializer<DeletedCarEvent>
    {
        public Task<byte[]> SerializeAsync(DeletedCarEvent data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Task.FromResult(Encoding.ASCII.GetBytes(json));
        }
    }
}
