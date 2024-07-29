using Global.CarManagement.Application.Features.Common;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand(Guid id)
        : CommandBase<DeleteCarCommand>(new DeleteCarCommandValidator()), IRequest<DeleteCarResponse>
    {
        public Guid Id { get; init; } = id;
    }
}
