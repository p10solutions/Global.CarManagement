using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.CarManagement.Domain.Models.Notifications;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Contracts.Events;
using Global.CarManagement.Application.Features.Cars.Commands.CreateCar;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Data;
using AutoMapper;
using Global.CarManagement.Domain.Models.Events.Cars;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarHandlerUnitTest
    {
        readonly Mock<ICarRepository> _CarRepository;
        readonly Mock<ICarProducer> _CarProducer;
        readonly Mock<ILogger<CreateCarHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly CreateCarHandler _handler;

        public CreateCarHandlerUnitTest()
        {
            _CarRepository = new Mock<ICarRepository>();
            _CarProducer = new Mock<ICarProducer>();
            _logger = new Mock<ILogger<CreateCarHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CreateCarMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new CreateCarHandler(_CarRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object, _unitOfWork.Object, _CarProducer.Object);
        }

        [Fact]
        public async Task Car_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var CarCommand = _fixture.Create<CreateCarCommand>();

            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(false);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.NotNull(response);
            _CarRepository.Verify(x => x.AddAsync(It.IsAny<Car>()), Times.Once);
            _CarProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedCarEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var CarCommand = _fixture.Create<CreateCarCommand>();

            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(false);
            _CarRepository.Setup(x => x.AddAsync(It.IsAny<Car>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);

            _CarRepository.Verify(x => x.AddAsync(It.IsAny<Car>()), Times.Once);
            _CarProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Created_When_Already_Exists()
        {
            var CarCommand = _fixture.Create<CreateCarCommand>();

            _CarRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);
            _CarRepository.Setup(x => x.AddAsync(It.IsAny<Car>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarCommand, CancellationToken.None);

            Assert.Null(response);

            _CarRepository.Verify(x => x.AddAsync(It.IsAny<Car>()), Times.Never);
            _CarProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedCarEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _CarRepository.Verify(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}
