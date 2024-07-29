using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.CarManagement.Domain.Models.Notifications;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Data;
using AutoMapper;
using Global.CarManagement.Domain.Models.Events.Cars;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarHandlerUnitTest
    {
        readonly Mock<ICarRepository> _CarRepository;
        readonly Mock<ICarProducer> _CarProducer;
        readonly Mock<ILogger<UpdatePhotoCarHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly UpdatePhotoCarHandler _handler;

        public UpdatePhotoCarHandlerUnitTest()
        {
            _CarRepository = new Mock<ICarRepository>();
            _CarProducer = new Mock<ICarProducer>();
            _logger = new Mock<ILogger<UpdatePhotoCarHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UpdatePhotoCarMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new UpdatePhotoCarHandler(_CarRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object, _unitOfWork.Object, _CarProducer.Object);
        }

        [Fact]
        public async Task Car_Should_Be_Updated_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var CarCommand = _fixture.Create<UpdatePhotoCarCommand>();
            var car = _fixture.Create<Car>();

            _CarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(car);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.NotNull(response);
            _CarRepository.Verify(x => x.Update(It.IsAny<Car>()), Times.Once);
            _CarProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedCarEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Updated_When_An_Exception_Was_Thrown()
        {
            var CarCommand = _fixture.Create<UpdatePhotoCarCommand>();
            var car = _fixture.Create<Car>();

            _CarRepository.Setup(x => x.Update(It.IsAny<Car>())).Throws(new Exception());
            _CarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(car);
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);

            _CarRepository.Verify(x => x.Update(It.IsAny<Car>()), Times.Once);
            _CarProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Updated_When_Not_Exists()
        {
            var CarCommand = _fixture.Create<UpdatePhotoCarCommand>();
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);

            _CarRepository.Verify(x => x.Update(It.IsAny<Car>()), Times.Never);
            _CarProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
