using Global.CarManagement.Application.Features.Common;
using Global.CarManagement.Domain.Entities;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommand(Guid id, string name, string details, double price, EStatus status, Guid brandId)
        : CommandBase<UpdateCarCommand>(new UpdateCarCommandValidator()), IRequest<UpdateCarResponse>
    {
        public Guid Id { get; init; } = id;
        public string Name { get; init; } = name;
        public string Details { get; init; } = details;
        public double Price { get; init; } = price;
        public EStatus Status { get; init; } = status;
        public Guid BrandId { get; init; } = brandId;
    }
}
