using AutoFixture;
using AutoMapper;
using Global.CarManagement.Application.Features.Cars.Queries.GetCarById;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdUnitTest
    {
        readonly Mock<ICarRepository> _CarRepository;
        readonly Mock<ILogger<GetCarByIdHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetCarByIdHandler _handler;

        public GetCarByIdUnitTest()
        {
            _CarRepository = new Mock<ICarRepository>();
            _logger = new Mock<ILogger<GetCarByIdHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetCarByIdMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetCarByIdHandler(_CarRepository.Object, _logger.Object, _notificationsHandler.Object, mapper);
        }

        [Fact]
        public async Task Car_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var CarQuery = _fixture.Create<GetCarByIdQuery>();
            var Car = _fixture.Create<Car>();
            _CarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(Car);

            var response = await _handler.Handle(CarQuery, CancellationToken.None);

            Assert.NotNull(response);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Geted_When_Not_Found()
        {
            var CarQuery = _fixture.Create<GetCarByIdQuery>();
            var Car = _fixture.Create<Car>();
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);

            var response = await _handler.Handle(CarQuery, CancellationToken.None);

            Assert.Null(response);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var CarQuery = _fixture.Create<GetCarByIdQuery>();
            _CarRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(CarQuery, CancellationToken.None);

            Assert.Null(response);
            _CarRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
