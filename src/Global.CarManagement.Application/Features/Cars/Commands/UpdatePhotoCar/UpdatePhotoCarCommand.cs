using Global.CarManagement.Application.Features.Common;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarCommand(Guid id, string photo)
        : CommandBase<UpdatePhotoCarCommand>(new UpdatePhotoCarCommandValidator()), IRequest<UpdatePhotoCarResponse>
    {
        public Guid Id { get; init; } = id;
        public string Photo { get; init; } = photo;
    }
}
