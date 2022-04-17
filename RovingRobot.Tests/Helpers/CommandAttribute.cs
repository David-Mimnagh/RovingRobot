using Xunit;

namespace RovingRobot.Tests.Helpers
{
    public class CommandAttribute
    {
        [Theory]
        [InlineData("PLACE")]
        [InlineData("ROTATION")]
        [InlineData("REPORT")]
        [InlineData("MOVE")]
        public void ShouldCorrectlyConstructTheCommandAttribute(string commandType)
        {
            var result = new RovingRobot.Helpers.CommandAttribute(commandType);

            Assert.Equal(result.CommandType, commandType);
        }
    }
}
