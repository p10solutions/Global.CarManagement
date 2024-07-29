using Global.CarManagement.Application.Features.Common;
using Global.CarManagement.Domain.Models.Pagination;
using MediatR;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarQuery : CommandBase<GetCarQuery>, IRequest<GetCarPaginatedResponse>
    {
        public GetCarQuery(string name, int pageSize, int currentPage) : base(new GetCarQueryValidator())
        {
             Name =  name;
             Paginator = new Paginator(pageSize, currentPage);
        }

        public string? Name { get; set; }
        public Paginator Paginator { get; }
    }
}
