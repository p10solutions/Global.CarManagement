namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarPaginatedResponse
    {
        public int Count { get; set; }
        public IEnumerable<GetCarResponse> Cars { get; set; }
    }
}
