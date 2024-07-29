using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Commands.CreateCar;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarCommandUnitTest
    {
        readonly Fixture _fixture;

        public CreateCarCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<CreateCarCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid brandId = Guid.Empty;
            EStatus status = EStatus.Active;

            var command = new CreateCarCommand(
                "Civic",
                "Honda Civic",
                19.99,
                status,
                brandId,
                string.Empty
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
