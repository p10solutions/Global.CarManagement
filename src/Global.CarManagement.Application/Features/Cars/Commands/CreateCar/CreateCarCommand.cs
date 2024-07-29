using Global.CarManagement.Application.Features.Common;
using Global.CarManagement.Domain.Entities;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarCommand(string name, string details, double price, EStatus status, Guid brandId, string photo) 
        : CommandBase<CreateCarCommand>(new CreateCarCommandValidator()), IRequest<CreateCarResponse>
    {
        public string Name { get; init; } = name;
        public string Details { get; init; } = details;
        public double Price { get; init; } = price;
        public EStatus Status { get; init; } = status;
        public Guid BrandId { get; init; } = brandId;
        public string Photo { get; set; } = photo;
    }
}
