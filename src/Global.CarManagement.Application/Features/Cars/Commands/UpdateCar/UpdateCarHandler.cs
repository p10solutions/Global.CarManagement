using AutoMapper;
using Global.CarManagement.Domain.Contracts.Data;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Events.Cars;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, UpdateCarResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<UpdateCarHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly ICarProducer _carProducer;

        public UpdateCarHandler(ICarRepository carRepository, ILogger<UpdateCarHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, ICarProducer carProducer)
        {
            _carRepository = carRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _carProducer = carProducer;
        }

        public async Task<UpdateCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);

            try
            {
                if (!await _carRepository.ExistsAsync(request.Id))
                {
                    _logger.LogWarning("No cars were found for the id: {CarId}", request.Id);

                    return _notificationsHandler
                        .AddNotification("No cars were found", ENotificationType.NotFound)
                        .ReturnDefault<UpdateCarResponse>();
                }

                if (await _carRepository.ExistsAsync(car.Name, car.BrandId, car.Id))
                {
                    _logger.LogWarning("There is already a Car with that name: {CarName}", request.Name);

                    return _notificationsHandler
                        .AddNotification("There is already a Car with that name", ENotificationType.BusinessValidation)
                        .ReturnDefault<UpdateCarResponse>();
                }

                _carRepository.UpdatePartial(car);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<UpdatedCarEvent>(car);

                await _carProducer.SendUpdatedEventAsync(@event);

                var response = _mapper.Map<UpdateCarResponse>(car);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to update the Car: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to insert the Car", ENotificationType.InternalError)
                     .ReturnDefault<UpdateCarResponse>();
            }
        }
    }
}
