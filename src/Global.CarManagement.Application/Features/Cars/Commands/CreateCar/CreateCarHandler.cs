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

namespace Global.CarManagement.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarHandler : IRequestHandler<CreateCarCommand, CreateCarResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<CreateCarHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly ICarProducer _carProducer;

        public CreateCarHandler(ICarRepository CarRepository, ILogger<CreateCarHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, ICarProducer carProducer)
        {
            _carRepository = CarRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _carProducer = carProducer;
        }

        public async Task<CreateCarResponse> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);

            try
            {
                if (await _carRepository.ExistsAsync(car.Name, car.BrandId, car.Id))
                {
                    _logger.LogWarning("There is already a Car with that name: {CarName}", request.Name);

                    return _notificationsHandler
                        .AddNotification("There is already a Car with that name", ENotificationType.BusinessValidation)
                        .ReturnDefault<CreateCarResponse>();
                }

                await _carRepository.AddAsync(car);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<CreatedCarEvent>(car);

                await _carProducer.SendCreatedEventAsync(@event);

                var response = _mapper.Map<CreateCarResponse>(car);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to insert the Car: {Exception}", ex.Message);

               return _notificationsHandler
                    .AddNotification("An error occurred when trying to insert the Car", ENotificationType.InternalError)
                    .ReturnDefault<CreateCarResponse>();
            }
        }
    }
}
