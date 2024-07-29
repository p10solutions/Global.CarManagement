using Global.CarManagement.Application.Features.Common;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdQuery : CommandBase<GetCarByIdQuery>, IRequest<GetCarByIdResponse>
    {
        public GetCarByIdQuery(Guid id) : base(new GetCarByIdQueryValidator())
        {
             Id =  id;
        }

        public Guid Id { get; set; }
    }
}
