using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Commands.DeleteCar;
using Global.CarManagement.Domain.Entities;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandUnitTest
    {
        readonly Fixture _fixture;

        public DeleteCarCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<DeleteCarCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid id = Guid.Empty;

            var command = new DeleteCarCommand(
                id
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
