using AutoMapper;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.CarManagement.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdHandler : IRequestHandler<GetCarByIdQuery, GetCarByIdResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<GetCarByIdHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;

        public GetCarByIdHandler(ICarRepository carRepository, ILogger<GetCarByIdHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper)
        {
            _carRepository = carRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
        }

        public async Task<GetCarByIdResponse> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var car = await _carRepository.GetByIdAsync(request.Id);

                if (car is null)
                {
                    _logger.LogWarning("No cars were found for the id: {CarId}", request.Id);

                    return _notificationsHandler
                        .AddNotification("No cars were found", ENotificationType.NotFound)
                        .ReturnDefault<GetCarByIdResponse>();
                }

                var response = _mapper.Map<GetCarByIdResponse>(car);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the Car: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the Car", ENotificationType.InternalError)
                        .ReturnDefault<GetCarByIdResponse>();
            }
        }
    }
}
