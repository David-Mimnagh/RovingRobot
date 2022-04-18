using System;
using System.IO;
using Xunit;

namespace RovingRobot.Tests.Commands
{
    public class ReportCommand
    {
        readonly RovingRobot.Commands.ReportCommand _reportCommand;
        readonly RovingRobot.Helpers.CommandValidator _commandValidator;
        readonly RovingRobot.Models.Robot _robot;
        readonly Files.TestData _testData;

        public ReportCommand()
        {
            _commandValidator = new RovingRobot.Helpers.CommandValidator();
            _robot = new RovingRobot.Models.Robot();
            _testData = new Files.TestData();
            _reportCommand = new RovingRobot.Commands.ReportCommand(_robot, _commandValidator);
        }

        [Fact]
        public void ShouldCorrectlyConstructTheReportCommand()
        {
            System.Tuple<int, int> validPos = new System.Tuple<int, int>(3, 1);
            RovingRobot.Models.Robot newRobotWithDifferentPos = new RovingRobot.Models.Robot()
            {
                CurrentPosition = validPos,
                StartingPosition = validPos,
                FacingDirection = "EAST"
            };
            RovingRobot.Commands.ReportCommand testMoveCommand = new RovingRobot.Commands.ReportCommand(newRobotWithDifferentPos, _commandValidator);
            Assert.Equal(testMoveCommand.Robot, newRobotWithDifferentPos); 
            Assert.Equal(testMoveCommand.CommandValidator, _commandValidator);

        }

        [Fact]
        public void ShouldLogTheCorrectInformationToTheConsole()
        {
            StringWriter output = new StringWriter();
            Console.SetOut(output);
            int expectedMoveCounter = 5;
            int expectedTableBoundryHits = 3;
            _reportCommand.Robot.MoveCommandCounter = expectedMoveCounter;
            _reportCommand.Robot.TableBoundryHits = expectedTableBoundryHits;
            _reportCommand.ExecuteCommand();

            String outputString = output.ToString();
            Assert.Contains("Executing Rover Report...", outputString);
            Assert.Contains("Analyzing...", outputString);
            Assert.Contains($"Rover is has moved a total of {expectedMoveCounter} times", outputString);
            Assert.Contains($"Rover's safety system engaged a total of {expectedTableBoundryHits} times to stop him falling.", outputString);
        }

    }
}
