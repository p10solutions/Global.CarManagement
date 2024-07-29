using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
    {
        public GetCarByIdQueryValidator()
        {
           RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
