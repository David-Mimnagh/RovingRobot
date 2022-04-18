using Xunit;

namespace RovingRobot.Tests.Commands
{
    public class PlaceCommand
    {
        readonly RovingRobot.Commands.PlaceCommand _placeCommand;
        readonly RovingRobot.Helpers.CommandValidator _commandValidator;
        readonly RovingRobot.Models.Robot _robot;
        readonly Files.TestData _testData;

        public PlaceCommand()
        {
            _commandValidator = new RovingRobot.Helpers.CommandValidator();
            _testData = new Files.TestData();
            _robot = new RovingRobot.Models.Robot();
            _placeCommand = new RovingRobot.Commands.PlaceCommand(_robot, _commandValidator);
        }

        [Fact]
        public void ShouldCorrectlyConstructThePlaceCommand()
        {
            System.Tuple<int, int> validPos = new System.Tuple<int, int>(3, 1);
            RovingRobot.Models.Robot newRobotWithDifferentPos = new RovingRobot.Models.Robot()
            {
                CurrentPosition = validPos,
                StartingPosition = validPos,
                FacingDirection = "EAST"
            };
            RovingRobot.Commands.PlaceCommand testPlaceCommand = new RovingRobot.Commands.PlaceCommand(newRobotWithDifferentPos, _commandValidator);
            Assert.Equal(testPlaceCommand.Robot, newRobotWithDifferentPos);
        }

        [Fact]
        public void ShouldNotPlaceTheRobotWhenTheSubCommandIsEmptyOrMalformedAndLogAnError()
        {
            _placeCommand.ExecuteCommand(null);
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);

            _placeCommand.ExecuteCommand("");
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);

            _placeCommand.ExecuteCommand("1 2 NORTH");
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);

            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);

            _placeCommand.ExecuteCommand("12NORTH");

            var outputString = output.ToString();
            Assert.Contains("Invalid Place command found.", outputString);
            Assert.Contains("Please provide a following sub command for and primary place command", outputString);
            Assert.Contains("in the following format X,Y,FACING DIRECTION(NORTH,EAST,SOUTH, OR WEST)", outputString);
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void ShouldNotPlaceTheRobotWhenTheSubCommandAnInvalidPositionAndLogAnError()
        {
            _placeCommand.ExecuteCommand("1000,1000,NORTH");
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);

            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);

            _placeCommand.ExecuteCommand("-1,-1,NORTH");

            var outputString = output.ToString();
            Assert.Contains("Invalid subcommand found following place command", outputString);
            Assert.Contains("Please provide a positive X and Y position within the bounds of the board", outputString);
            Assert.Contains("Please provide a valid face direction (NORTH, EAST, SOUTH, OR WEST)", outputString);
            Assert.Equal(_placeCommand.Robot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void ShouldCorrectlyPlaceTheRobotWhenAValidSubCommandIsProvided_UpdatingStartingPosOnFirstPlace()
        {
            int xPos = 1;
            int yPos = 1;
            string facingDir = "SOUTH";
            _placeCommand.ExecuteCommand($"{xPos},{yPos},{facingDir}");
            RovingRobot.Models.Robot expectedRobot = new RovingRobot.Models.Robot()
            {
                CurrentPosition = new System.Tuple<int, int>(xPos, yPos),
                FacingDirection = facingDir,
                StartingPosition = new System.Tuple<int, int>(xPos, yPos)
            };
            Assert.Equal(_placeCommand.Robot.CurrentPosition, expectedRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, expectedRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, expectedRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, expectedRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, expectedRobot.StartingPosition);
        }

        [Fact]
        public void ShouldCorrectlyPlaceTheRobotWhenAValidSubCommandIsProvided_LeavingStartingPosAsIsOnSecondPlace()
        {
            System.Tuple<int, int> newStartingPos = new System.Tuple<int, int>(3, 3);
            _placeCommand.Robot.StartingPosition = newStartingPos;
            int xPos = 2;
            int yPos = 1;
            string facingDir = "EAST";
            _placeCommand.ExecuteCommand($"{xPos},{yPos},{facingDir}");
            RovingRobot.Models.Robot expectedRobot = new RovingRobot.Models.Robot()
            {
                CurrentPosition = new System.Tuple<int, int>(xPos, yPos),
                FacingDirection = facingDir,
                StartingPosition = newStartingPos
            };
            Assert.Equal(_placeCommand.Robot.CurrentPosition, expectedRobot.CurrentPosition);
            Assert.Equal(_placeCommand.Robot.FacingDirection, expectedRobot.FacingDirection);
            Assert.Equal(_placeCommand.Robot.MoveCommandCounter, expectedRobot.MoveCommandCounter);
            Assert.Equal(_placeCommand.Robot.TableBoundryHits, expectedRobot.TableBoundryHits);
            Assert.Equal(_placeCommand.Robot.StartingPosition, expectedRobot.StartingPosition);
        }
    }
}
