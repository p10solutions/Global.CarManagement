using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Domain.Models.Events.Cars;

namespace Global.CarManagement.Domain.Contracts.Events
{
    public interface ICarProducer
    {
        Task SendCreatedEventAsync(CreatedCarEvent @event);
        Task SendUpdatedEventAsync(UpdatedCarEvent @event);
        Task SendDeletedEventAsync(DeletedCarEvent @event);
    }
}
