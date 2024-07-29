using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarCommandValidator : AbstractValidator<UpdatePhotoCarCommand>
    {
        public UpdatePhotoCarCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Photo).NotEmpty();
        }
    }
}
