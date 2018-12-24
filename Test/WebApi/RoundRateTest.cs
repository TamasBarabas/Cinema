using NUnit.Framework;
using WebApi.Helpers;

namespace Test.WebApi
{
    public class RoundRateTests
    {
        RoundRate round = new RoundRate();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(2.91, 3.0)]
        [TestCase(3.249, 3.0)]
        [TestCase(3.25, 3.5)]
        [TestCase(3.6, 3.5)]
        [TestCase(3.75, 4.0)]
        public void Test_ShouldReturnListOfMovies(double input, double expectedResult)
        {
            Assert.AreEqual(expectedResult, round.Round(input));
        }


    }
}