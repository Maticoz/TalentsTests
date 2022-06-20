using Xunit;
using TalentsApi.Services;
using TalentsApi.Services.IServices;

namespace TalentsApiTests
{
    public class RandomGeneratorServiceTest
    {
        IRandomGeneratorService randomGeneratorService = new RandomGeneratorService();

        [Theory]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public void RandomString_RandomString_ReturnsNotEqual(int length)
        {
            Assert.NotEqual(randomGeneratorService.RandomString(length), randomGeneratorService.RandomString(length));
        }


        [Theory]
        [InlineData(-100, 100)]
        [InlineData(-1000, 1000)]
        [InlineData(-5000, 5000)]
        [InlineData(-10000, 10000)]
        [InlineData(-20000, 20000)]
        public void RandomString_RandomNumbers_ReturnsNotEqual(int min, int max)
        {
            Assert.NotEqual(randomGeneratorService.RandomNumber(min, max), randomGeneratorService.RandomNumber(min,max));
        }


        [Fact]
        public void RandomString_RandomPassword_ReturnsNotEqual()
        {
            Assert.NotEqual(randomGeneratorService.RandomPassword(), randomGeneratorService.RandomPassword());
        }
    }
}