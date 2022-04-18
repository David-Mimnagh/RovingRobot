using System;
using System.IO;
using Xunit;

namespace RovingRobot.Tests.Commands
{
    public class RotateCommand
    {
        readonly RovingRobot.Commands.RotateCommand _rotateCommand;
        readonly RovingRobot.Helpers.CommandValidator _commandValidator;
        readonly RovingRobot.Models.Robot _robot;
        readonly Files.TestData _testData;

        public RotateCommand()
        {
            _commandValidator = new RovingRobot.Helpers.CommandValidator();
            _robot = new RovingRobot.Models.Robot();
            _testData = new Files.TestData();
            _rotateCommand = new RovingRobot.Commands.RotateCommand(_robot, _commandValidator);
        }

        [Fact]
        public void ShouldCorrectlyConstructTheRotateCommand()
        {
            System.Tuple<int, int> validPos = new System.Tuple<int, int>(3, 1);
            RovingRobot.Models.Robot newRobotWithDifferentPos = new RovingRobot.Models.Robot()
            {
                CurrentPosition = validPos,
                StartingPosition = validPos,
                FacingDirection = "EAST"
            };
            RovingRobot.Commands.RotateCommand testMoveCommand = new RovingRobot.Commands.RotateCommand(newRobotWithDifferentPos, _commandValidator);
            Assert.Equal(testMoveCommand.Robot, newRobotWithDifferentPos);
            Assert.Equal(testMoveCommand.CommandValidator, _commandValidator);

        }

        [Fact]
        public void ShouldNotRotateTheRobotWhenTheCurrentPositionIsInvalidAndLogAnError()
        {
            StringWriter output = new StringWriter();
            Console.SetOut(output);
            _rotateCommand.ExecuteCommand();

            var outputString = output.ToString();
            Assert.Contains("Ignoring ROTATION command, as Rover isn't in a valid position", outputString);
            Assert.Equal(_rotateCommand.Robot.FacingDirection, new RovingRobot.Models.Robot().FacingDirection);
        }

        [Fact]
        public void ShouldNotRotateTheRobotWhenTheRotationSubCommandIsInvalidAndLogAnError()
        {
            string invalidSubCommand = "INVALID";
            StringWriter output = new StringWriter();
            Console.SetOut(output);
            _rotateCommand.Robot = new RovingRobot.Models.Robot()
            {
                CurrentPosition = new Tuple<int, int>(2, 2)
            };
            _rotateCommand.ExecuteCommand(invalidSubCommand);

            var outputString = output.ToString();
            Assert.Contains($"Ignoring ROTATION command, as {invalidSubCommand} is not valid", outputString);
            Assert.Equal(_rotateCommand.Robot.FacingDirection, new RovingRobot.Models.Robot().FacingDirection);

        }

        [Fact]
        public void ShouldCorrectlyRotateTheRobotWhenTheRotationSubCommandIsValid()
        {
            _rotateCommand.Robot = new RovingRobot.Models.Robot()
            {
                CurrentPosition = new Tuple<int, int>(2, 2),
                FacingDirection = "SOUTH"
            };
            string expectedNewFacingDirection = "WEST";
            _rotateCommand.ExecuteCommand("RIGHT");

            Assert.Equal(_rotateCommand.Robot.FacingDirection, expectedNewFacingDirection);
        }
    }
}
