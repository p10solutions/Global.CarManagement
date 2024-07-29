namespace Global.CarManagement.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}
