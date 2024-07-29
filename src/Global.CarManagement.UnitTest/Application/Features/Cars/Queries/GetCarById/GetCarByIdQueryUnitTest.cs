using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Queries.GetCarById;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Queries.GetCarById
{
    public class GetCarByIdQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetCarByIdQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetCarByIdQuery>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new GetCarByIdQuery(Guid.Empty);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
