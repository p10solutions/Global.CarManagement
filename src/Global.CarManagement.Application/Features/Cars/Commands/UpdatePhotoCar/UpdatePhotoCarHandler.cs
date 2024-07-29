using AutoMapper;
using Global.CarManagement.Domain.Contracts.Data;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Models.Events.Cars;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarHandler : IRequestHandler<UpdatePhotoCarCommand, UpdatePhotoCarResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<UpdatePhotoCarHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly ICarProducer _carProducer;

        public UpdatePhotoCarHandler(ICarRepository carRepository, ILogger<UpdatePhotoCarHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, ICarProducer carProducer)
        {
            _carRepository = carRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _carProducer = carProducer;
        }

        public async Task<UpdatePhotoCarResponse> Handle(UpdatePhotoCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var car = await _carRepository.GetByIdAsync(request.Id);

                if (car is null)
                {
                    _logger.LogWarning("No cars were found for the id: {CarId}", request.Id);

                    return _notificationsHandler
                        .AddNotification("No cars were found", ENotificationType.NotFound)
                        .ReturnDefault<UpdatePhotoCarResponse>();
                }

                car.Photo.Base64 = request.Photo;

                _carRepository.Update(car);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<UpdatedCarEvent>(car);

                await _carProducer.SendUpdatedEventAsync(@event);

                var response = _mapper.Map<UpdatePhotoCarResponse>(car);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to update the Car: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to insert the Car", ENotificationType.InternalError)
                     .ReturnDefault<UpdatePhotoCarResponse>();
            }
        }
    }
}
