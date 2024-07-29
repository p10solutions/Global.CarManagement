using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Commands.UpdateCar;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandUnitTest
    {
        readonly Fixture _fixture;

        public UpdateCarCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<UpdateCarCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid brandId = Guid.Empty;
            EStatus status = EStatus.Active;
            Guid id = Guid.Empty;

            var command = new UpdateCarCommand(
                id,
                "Corolla",
                "Toyota Corolla",
                19.99,
                status,
                brandId
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
