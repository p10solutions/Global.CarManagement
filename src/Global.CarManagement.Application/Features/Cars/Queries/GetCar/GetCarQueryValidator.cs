using FluentValidation;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarQueryValidator : AbstractValidator<GetCarQuery>
    {
        public GetCarQueryValidator()
        {
        }
    }
}
