using AutoMapper;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCar
{
    public class GetCarHandler : IRequestHandler<GetCarQuery, GetCarPaginatedResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<GetCarHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;

        public GetCarHandler(ICarRepository carRepository, ILogger<GetCarHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper)
        {
            _carRepository = carRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
        }

        public async Task<GetCarPaginatedResponse> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            try
            {
               var paginatedCars = await _carRepository.GetAsync(request.Name, request.Paginator);

                var responseCars = _mapper.Map<IEnumerable<GetCarResponse>>(paginatedCars.Item1);

                var response = new GetCarPaginatedResponse { Cars = responseCars, Count = paginatedCars.Item2 };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the Car: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the Car", ENotificationType.InternalError)
                        .ReturnDefault<GetCarPaginatedResponse>();
            }
        }
    }
}
