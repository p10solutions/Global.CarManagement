namespace Global.CarManagement.Application.Features.Cars.Commands.DeleteCar
{
    public class DeletedCarEvent(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}
