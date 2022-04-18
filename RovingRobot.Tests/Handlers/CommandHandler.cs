using Xunit;

namespace RovingRobot.Tests.Handlers
{
    public class CommandHandler
    {
        readonly RovingRobot.Handlers.CommandHandler _commandHandler;
        private readonly Files.TestData _testData;

        public CommandHandler()
        {
            _commandHandler = new RovingRobot.Handlers.CommandHandler();
            _testData = new Files.TestData();
        }

        [Fact]
        public void ShouldCorrectlyConstructAnInstanceOfCommandHandler()
        {
            RovingRobot.Handlers.CommandHandler testCommandHandler = new RovingRobot.Handlers.CommandHandler();
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(testCommandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(testCommandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(testCommandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(testCommandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(testCommandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void ExecuteCommand_DoesNotRunTheCommandIfItIsNullOrEmptyAndLogsAnError()
        {
            _commandHandler.RoverTheRovingRobot = new RovingRobot.Models.Robot();
            _commandHandler.ExecuteCommand(null);
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);

            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);

            _commandHandler.RoverTheRovingRobot = new RovingRobot.Models.Robot();
            _commandHandler.ExecuteCommand("");

            var outputString = output.ToString();
            Assert.Contains("Invalid command. Ignoring", outputString);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void ExecuteCommand_ShouldCorrectlyExecuteACommand()
        {
            _commandHandler.RoverTheRovingRobot = new RovingRobot.Models.Robot();
            int xPos = 1;
            int yPos = 1;
            string facingDir = "SOUTH";
            _commandHandler.ExecuteCommand($"PLACE {xPos},{yPos},{facingDir}");
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot() 
            {
                CurrentPosition = new System.Tuple<int, int>(xPos, yPos),
                FacingDirection = facingDir,
                StartingPosition = new System.Tuple<int, int>(xPos, yPos)
            };
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void RunCommandList_DoesNotRunIfCommandsIsEmptyOrInvalid()
        {
            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);

            _commandHandler.RunCommandList(null);

            var outputString = output.ToString();
            Assert.Contains("Invalid command list. Please make sure your command list contains a place command", outputString);
            RovingRobot.Models.Robot newRobot = new RovingRobot.Models.Robot();
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);

            _commandHandler.RunCommandList(_testData.InvalidCommandList1);

            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, newRobot.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, newRobot.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, newRobot.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, newRobot.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, newRobot.StartingPosition);
        }

        [Fact]
        public void RunCommandList_RunsCorrectlyWhenGivenAValidCommandListAndCorrectlyLogsTheCommand()
        {
            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);
            _commandHandler.RunCommandList(_testData.TestCommandSet_1CommandList);
            var outputString = output.ToString();

            Assert.Contains("====================================================================================", outputString);
            foreach (string command in _testData.TestCommandSet_1CommandList)
            {
                Assert.Contains($"\t\t ---- Processing new command: {command} ---- ", outputString);
            }

            Assert.Contains("====================================================================================", outputString);

            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, _testData.RobotAfterCommandSet1.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, _testData.RobotAfterCommandSet1.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, _testData.RobotAfterCommandSet1.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, _testData.RobotAfterCommandSet1.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, _testData.RobotAfterCommandSet1.StartingPosition);

            _commandHandler.RoverTheRovingRobot = new RovingRobot.Models.Robot();
            _commandHandler.RunCommandList(_testData.TestCommandSet_2CommandList);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, _testData.RobotAfterCommandSet2.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, _testData.RobotAfterCommandSet2.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, _testData.RobotAfterCommandSet2.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, _testData.RobotAfterCommandSet2.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, _testData.RobotAfterCommandSet2.StartingPosition);

            _commandHandler.RoverTheRovingRobot = new RovingRobot.Models.Robot();
            _commandHandler.RunCommandList(_testData.TestCommandSet_3CommandList);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.CurrentPosition, _testData.RobotAfterCommandSet3.CurrentPosition);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.FacingDirection, _testData.RobotAfterCommandSet3.FacingDirection);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.MoveCommandCounter, _testData.RobotAfterCommandSet3.MoveCommandCounter);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.TableBoundryHits, _testData.RobotAfterCommandSet3.TableBoundryHits);
            Assert.Equal(_commandHandler.RoverTheRovingRobot.StartingPosition, _testData.RobotAfterCommandSet3.StartingPosition);
        }
    }
}
