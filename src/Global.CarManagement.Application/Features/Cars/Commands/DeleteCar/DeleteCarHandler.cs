using Global.CarManagement.Domain.Contracts.Data;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.CarManagement.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, DeleteCarResponse>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<DeleteCarHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly ICarProducer _CarProducer;

        public DeleteCarHandler(ICarRepository carRepository, ILogger<DeleteCarHandler> logger,
            INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork,
            ICarProducer CarProducer)
        {
            _carRepository = carRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _CarProducer = CarProducer;
        }

        public async Task<DeleteCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _carRepository.ExistsAsync(request.Id))
                {
                    _logger.LogWarning("No cars were found for the id: {CarId}", request.Id);

                    return _notificationsHandler
                        .AddNotification("No cars were found", ENotificationType.NotFound)
                        .ReturnDefault<DeleteCarResponse>();
                }

                _carRepository.Delete(request.Id);
                await _unitOfWork.CommitAsync();

                await _CarProducer.SendDeletedEventAsync(new DeletedCarEvent(request.Id));

                return new DeleteCarResponse(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to delete the Car: {Exception}", ex.Message);

                return _notificationsHandler
                     .AddNotification("An error occurred when trying to delete the Car", ENotificationType.InternalError)
                     .ReturnDefault<DeleteCarResponse>();
            }
        }
    }
}
