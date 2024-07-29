using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(2, 200);
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x=>x.Status).NotEmpty();
        }
    }
}
