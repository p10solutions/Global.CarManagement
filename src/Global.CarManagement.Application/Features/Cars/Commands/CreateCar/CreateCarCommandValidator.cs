using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(2, 200);
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x=>x.Status).NotEmpty();
            RuleFor(x=>x.Photo).NotEmpty();
        }
    }
}
