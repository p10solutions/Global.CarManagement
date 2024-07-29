using AutoFixture;
using Global.CarManagement.Application.Features.Cars.Queries.GetCar;

namespace Global.CarManagement.UnitTest.Application.Features.Cars.Queries.GetCar
{
    public class GetCarQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetCarQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetCarQuery>();

            var result = command.Validate();

            Assert.True(result);
        }
    }
}
