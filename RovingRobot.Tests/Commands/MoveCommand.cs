using Xunit;

namespace RovingRobot.Tests.Commands
{
    public class MoveCommand
    {
        readonly RovingRobot.Commands.MoveCommand _moveCommand;
        readonly RovingRobot.Helpers.CommandValidator _commandValidator;
        readonly RovingRobot.Models.Robot _robot;
        readonly Files.TestData _testData;

        public MoveCommand()
        {
            _commandValidator = new RovingRobot.Helpers.CommandValidator();
            _testData = new Files.TestData();
            _robot = new RovingRobot.Models.Robot();
            _moveCommand = new RovingRobot.Commands.MoveCommand(_robot, _commandValidator);
        }

        [Fact]
        public void ShouldCorrectlyConstructTheMoveCommand()
        {
            System.Tuple<int, int> validPos = new System.Tuple<int, int>(3, 1);
            RovingRobot.Models.Robot newRobotWithDifferentPos = new RovingRobot.Models.Robot()
            {
                CurrentPosition = validPos,
                StartingPosition = validPos,
                FacingDirection = "EAST"
            };
            RovingRobot.Commands.MoveCommand testMoveCommand = new RovingRobot.Commands.MoveCommand(newRobotWithDifferentPos, _commandValidator);
            Assert.Equal(testMoveCommand.Robot, newRobotWithDifferentPos);
        }

        [Fact]
        public void ShouldNotMoveTheRobotWhenItIsNotOnTheBoard()
        {
            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);

            _moveCommand.ExecuteCommand();

            var outputString = output.ToString();
            Assert.Contains("Ignoring MOVE command, as Rover isn't in a valid position", outputString);

            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(_moveCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_moveCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_moveCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_moveCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_moveCommand.Robot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void ShouldCorrectlyMoveTheRobotWhenItIsOnTheBoard()
        {
            System.Tuple<int, int> validPos = new System.Tuple<int, int>(1, 1);
            _moveCommand.Robot = new RovingRobot.Models.Robot() 
            { 
                CurrentPosition = validPos,
                StartingPosition = validPos,
                FacingDirection = "EAST"
            };
            System.Tuple<int, int> expectedPos = new System.Tuple<int, int>(2, 1);
            _moveCommand.ExecuteCommand();
            Assert.Equal(_moveCommand.Robot.CurrentPosition, expectedPos);
            Assert.Equal(_moveCommand.Robot.StartingPosition, validPos);
        }
    }
}
