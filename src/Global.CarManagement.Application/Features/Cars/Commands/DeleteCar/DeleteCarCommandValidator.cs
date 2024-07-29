using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
