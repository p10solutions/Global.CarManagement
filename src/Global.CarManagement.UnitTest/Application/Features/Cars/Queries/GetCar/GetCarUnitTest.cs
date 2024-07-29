using AutoFixture;
using AutoMapper;
using Global.CarManagement.Application.Features.Cars.Queries.GetCar;
using Global.CarManagement.Domain.Contracts.Data.Repositories;
using Global.CarManagement.Domain.Contracts.Notifications;
using Global.CarManagement.Domain.Entities;
using Global.CarManagement.Domain.Models.Notifications;
using Global.CarManagement.Domain.Models.Pagination;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Queries.GetCar
{
    public class GetCarUnitTest
    {
        readonly Mock<ICarRepository> _carRepository;
        readonly Mock<ILogger<GetCarHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetCarHandler _handler;

        public GetCarUnitTest()
        {
            _carRepository = new Mock<ICarRepository>();
            _logger = new Mock<ILogger<GetCarHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetCarMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetCarHandler(_carRepository.Object, _logger.Object, _notificationsHandler.Object, mapper);
        }

        [Fact]
        public async Task Car_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var CarQuery = _fixture.Create<GetCarQuery>();
            var Cars = _fixture.CreateMany<Car>();
            _carRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<Paginator>())).ReturnsAsync((Cars, Cars.Count()));

            var response = await _handler.Handle(CarQuery, CancellationToken.None);

            Assert.NotNull(response);
            _carRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<Paginator>()), Times.Once);
        }

        [Fact]
        public async Task Car_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var CarQuery = _fixture.Create<GetCarQuery>();
            _carRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<Paginator>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(CarQuery, CancellationToken.None);

            Assert.Null(response);
            _carRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<Paginator>()), Times.Once);
        }
    }
}
