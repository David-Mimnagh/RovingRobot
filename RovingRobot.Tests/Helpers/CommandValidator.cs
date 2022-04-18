using System.Collections.Generic;
using Xunit;

namespace RovingRobot.Tests.Helpers
{
    public class CommandValidator
    {
        private readonly RovingRobot.Helpers.CommandValidator _commandValidator;
        private readonly Files.TestData _testData;

        public CommandValidator()
        {
            _commandValidator = new RovingRobot.Helpers.CommandValidator();
            _testData = new Files.TestData();
        }

        [Theory]
        [InlineData("place","PLACE")]
        [InlineData(" mo Ve", "MOVE")]
        [InlineData(" left", "LEFT")]
        [InlineData(" Right ", "RIGHT")]
        [InlineData(" REPORT ", "REPORT")]
        public void PrepareCommand_ReturnsTheCorrectlyPreparedCommand(string nonPreparedCommand, string preparedCommand)
        {
            var result = _commandValidator.PrepareCommand(nonPreparedCommand);

            Assert.Equal(result, preparedCommand);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("RIGHT", true)]
        [InlineData("LEFT", true)]
        [InlineData("HARLEY", false)]
        [InlineData("DAVIDSON", false)]
        public void IsValidRotationCommand_ReturnsTrueForValidXYAndFalseForInvalid(string rotation, bool expected)
        {
            var result = _commandValidator.IsValidRotationCommand(rotation);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(3, 2, true)]
        [InlineData(6, 3, false)]
        [InlineData(7, 7, false)]
        [InlineData(-1, 2, false)]
        [InlineData(-3, -3, false)]
        public void IsValidPosition_ReturnsTrueForValidXYAndFalseForInvalid(int x, int y, bool expected)
        {
            var result = _commandValidator.IsValidPosition(x, y);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("NORTH", true)]
        [InlineData("EAST", true)]
        [InlineData("SOUTH", true)]
        [InlineData("WEST", true)]
        [InlineData("BOB", false)]
        [InlineData("ROSS", false)]
        public void IsValidDirectionCommand_ReturnsTrueForValidDirectionsAndFalseForInvalid(string direction, bool expected)
        {
            var result = _commandValidator.IsValidDirectionCommand(direction);

            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("PLACE", true)]
        [InlineData("MOVE", true)]
        [InlineData("LEFT", true)]
        [InlineData("RIGHT", true)]
        [InlineData("REPORT", true)]
        [InlineData("JAMES", false)]
        [InlineData("BOND", false)]
        public void IsValidPrimaryCommand_ReturnsTrueForValidCommandsAndFalseForInvalid(string primaryCommand, bool expected)
        {
            var result = _commandValidator.IsValidPrimaryCommand(primaryCommand);

            Assert.Equal(result, expected);
        }
        [Theory]
        [InlineData(1,2,"NORTH", true)]
        [InlineData(4, 2, "SOUTH", true)]
        [InlineData(6, 2, "NORTH", false)]
        [InlineData(-1, 42, "ScoobyDoo", false)]
        [InlineData(1, 2, "Shaggy", false)]
        public void IsValidSubCommand_ReturnsTrueForValidCommandsAndFalseForInvalid(int xPos, int yPos, string newDirection, bool expected)
        {
            var result = _commandValidator.IsValidSubCommand(xPos,yPos, newDirection);
            Assert.Equal(result, expected);
        }

        [Fact]
        public void IsValidCommandList_ReturnsFalseWhenNullIsPassed()
        {
            var result = _commandValidator.IsValidCommandList(null);
            bool expected = false;
            Assert.Equal(result, expected);
        }

        [Fact]
        public void IsValidCommandList_ReturnsFalseAnEmptyListIsPassed()
        {
            var result = _commandValidator.IsValidCommandList(_testData.EmptyCommandList);

            bool expected = false;
            Assert.Equal(result, expected);
        }
        [Fact]
        public void IsValidCommandList_ReturnsFalseWhenAListWithNoPlaceCommandIsPassed()
        {
            var result1 = _commandValidator.IsValidCommandList(_testData.InvalidCommandList1);
            var result2 = _commandValidator.IsValidCommandList(_testData.InvalidCommandList2);

            bool expected = false;
            Assert.Equal(result1, expected);
            Assert.Equal(result2, expected);
        }
        [Fact]
        public void IsValidCommandList_ReturnsTrueWhenAValidListIsPassed()
        {
            var result1 = _commandValidator.IsValidCommandList(_testData.ValidCommandList1);
            var result2 = _commandValidator.IsValidCommandList(_testData.ValidCommandList2);

            bool expected = true;
            Assert.Equal(result1, expected);
            Assert.Equal(result2, expected);
        }
    }
}
