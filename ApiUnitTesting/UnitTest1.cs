using BusinessLayer.Dtos.CityEntity;
using BusinessLayer.Interfaces.CityService;
using Moq;
using Xunit;

namespace ApiUnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void CityFromService()
        {
            var serviceMock = new Mock<ICityService>();

            var cityFromService = serviceMock.Setup(s => s.GetOne(1));

            var model = new CityDto()
            {
                Id = 1,
                Name = "Best city in the world", 
                Description = "New York City"
            };

            Assert.Same(model, cityFromService);
        }
    }
}
