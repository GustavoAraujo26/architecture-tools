using ArchitectureTools.Enums;
using ArchitectureTools.Period;

namespace ArchitectureTools.Tests.StructTests
{
    public class ExpirationDateTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenCreateDate()
        {
            var expiresIn = new ExpirationDate(ExpirationTime.Minutes, 1500);
            
            bool valid = expiresIn.Check();
            Assert.True(valid);

            valid = expiresIn.Check(DateTime.Now.AddDays(10));
            Assert.False(valid);
        }

        [Fact]
        public void ShouldReturnSuccess_WhenGetDifference()
        {
            var expiresIn = new ExpirationDate(ExpirationTime.Years, 15);
            var now = DateTime.Now;

            var differenceInMiliseconds = expiresIn.GetDifference(ExpirationTime.Miliseconds, now);
            Assert.NotEqual(0, differenceInMiliseconds);
            var differenceInSeconds = expiresIn.GetDifference(ExpirationTime.Seconds, now);
            Assert.NotEqual(0, differenceInSeconds);
            var differenceInMinutes = expiresIn.GetDifference(ExpirationTime.Minutes, now);
            Assert.NotEqual(0, differenceInMinutes);
            var differenceInHours = expiresIn.GetDifference(ExpirationTime.Hours, now);
            Assert.NotEqual(0, differenceInHours);
            var differenceInDays = expiresIn.GetDifference(ExpirationTime.Days, now);
            Assert.NotEqual(0, differenceInDays);
            var differenceInMonths = expiresIn.GetDifference(ExpirationTime.Months, now);
            Assert.NotEqual(0, differenceInMonths);
            var differenceInYears = expiresIn.GetDifference(ExpirationTime.Years, now);
            Assert.NotEqual(0, differenceInYears);
        }
    }
}
