using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Commands.UpdatePhotoCar;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Commands.UpdatePhotoCar
{
    public class UpdatePhotoCarCommandUnitTest
    {
        readonly Fixture _fixture;

        public UpdatePhotoCarCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<UpdatePhotoCarCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid id = Guid.Empty;

            var command = new UpdatePhotoCarCommand(
                id,
                string.Empty
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
