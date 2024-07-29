using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.CarManagement.Domain.Models.Notifications;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Data;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarHandlerUnitTest
    {
        readonly Mock<ICarRepository> _CarRepository;
        readonly Mock<ICarProducer> _CarProducer;
        readonly Mock<ILogger<DeleteCarHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly DeleteCarHandler _handler;

        public DeleteCarHandlerUnitTest()
        {
            _CarRepository = new Mock<ICarRepository>();
            _CarProducer = new Mock<ICarProducer>();
            _logger = new Mock<ILogger<DeleteCarHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _fixture = new Fixture();
            _handler = new DeleteCarHandler(_CarRepository.Object, _logger.Object,
                _notificationsHandler.Object, _unitOfWork.Object, _CarProducer.Object);
        }

        [Fact]
        public async Task Car_Should_Be_Deleted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var CarCommand = _fixture.Create<DeleteCarCommand>();

            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.NotNull(response);
            _CarRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            _CarProducer.Verify(x => x.SendDeletedEventAsync(It.IsAny<DeletedCarEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Deleted_When_Not_Found()
        {
            var CarCommand = _fixture.Create<DeleteCarCommand>();

            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                .Returns(_notificationsHandler.Object);

            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);
            _CarRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
            _CarProducer.Verify(x => x.SendDeletedEventAsync(It.IsAny<DeletedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Deleted_When_An_Exception_Was_Thrown()
        {
            var CarCommand = _fixture.Create<DeleteCarCommand>();
            _CarRepository.Setup(x => x.Delete(It.IsAny<Guid>())).Throws(new Exception());
            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);

            _CarRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
            _CarProducer.Verify(x => x.SendDeletedEventAsync(It.IsAny<DeletedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
