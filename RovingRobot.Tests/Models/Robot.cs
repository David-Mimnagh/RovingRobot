using System;
using Xunit;

namespace RovingRobot.Tests.Models
{
    public class Robot
    {
        private static readonly Files.TestData _testData = new Files.TestData();
        public Robot()
        {
        }

        [Fact]
        public void ShouldCorrectlyConstructTheRobotClass()
        {
            var newRobot = new RovingRobot.Models.Robot();

            Tuple<int, int> startingPosition = new Tuple<int, int>(-1, -1);
            int defuaultHitsAndCounter = 0;
            Assert.Equal(newRobot.FacingDirection, RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND);
            Assert.Equal(newRobot.TableBoundryHits, defuaultHitsAndCounter);
            Assert.Equal(newRobot.MoveCommandCounter, defuaultHitsAndCounter);
            Assert.Equal(newRobot.StartingPosition, startingPosition);
            Assert.Equal(newRobot.CurrentPosition, startingPosition);
        }

        [Theory]
        [InlineData(0, false, true)]
        [InlineData(4, true, true)]
        [InlineData(0, true, false)]
        [InlineData(4, false, false)]
        [InlineData(1, false, false)]
        [InlineData(3, true, false)]
        public void ShouldCorrectlyIdentifyADangerousMoveAndIncrementTheTableBoundryHitsCounter(int currentBoardAxisPos, bool isMaxBoardCheck, bool expectedResponse)
        {
            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);
            int currentBoundryHits = _testData.startingRobot.TableBoundryHits;
            bool response = _testData.startingRobot.isDangerousMove(currentBoardAxisPos, isMaxBoardCheck);
            if (expectedResponse)
            {
                var outputString = output.ToString();
                Assert.Contains("Beep Boop... This move looks dangerous.", outputString);
                Assert.Contains("I think I'll stay where I am for now.", outputString);
                Assert.Contains("(Movement will take Rover off the board)", outputString);
                Assert.Equal(_testData.startingRobot.TableBoundryHits, currentBoundryHits+1);
            }

            Assert.Equal(response, expectedResponse);
        }
        [Fact]
        public void OutputCurrentPosition_ShouldCorrectlyLogTheCurrentPositionAndFacingDirection()
        {
            System.Tuple<int, int> testCurrentPos = new Tuple<int, int>(2, 3);
            string testFacingDirection = "SOUTH";
            System.IO.StringWriter output = new System.IO.StringWriter();
            System.Console.SetOut(output);
            RovingRobot.Models.Robot testRobot = new RovingRobot.Models.Robot()
            {
                CurrentPosition = testCurrentPos,
                FacingDirection = testFacingDirection
            };
            testRobot.OutputCurrentPosition();

            var outputString = output.ToString();
            Assert.Contains(Environment.NewLine, outputString);
            Assert.Contains($"Rover is currently facing {testFacingDirection}", outputString);
            Assert.Contains($"At position: X: {testCurrentPos.Item1}, Y: {testCurrentPos.Item2}", outputString);
            Assert.Contains(Environment.NewLine, outputString);
        }

        [Theory]
        [InlineData("LEFT", "NORTH", "WEST")]
        [InlineData("RIGHT", "NORTH", "EAST")]
        [InlineData("LEFT", "EAST", "NORTH")]
        [InlineData("RIGHT", "EAST", "SOUTH")]
        [InlineData("LEFT", "SOUTH", "EAST")]
        [InlineData("RIGHT", "SOUTH", "WEST")]
        [InlineData("LEFT", "WEST", "SOUTH")]
        [InlineData("RIGHT", "WEST", "NORTH")]
        public void ShouldHandleRotationCorrectlyBasedOnTheCurrentDirectionAndNewRotation(string rotationCommand, string currentFacingDirection, string expectedFacingDirection)
        {
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.HandleRotation(rotationCommand);

            Assert.Equal(_testData.startingRobot.FacingDirection, expectedFacingDirection);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_NORTH()
        {
            _testData.startingRobot.MoveCommandCounter = _testData.defaultCounter;
            Tuple<int, int> currentPos = new Tuple<int, int>(0,4);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter);

            currentPos = new Tuple<int, int>(0, 3);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND;
            expectedPos = new Tuple<int, int>(0, 4);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter + 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_EAST()
        {
            _testData.startingRobot.MoveCommandCounter = _testData.defaultCounter;
            Tuple<int, int> currentPos = new Tuple<int, int>(4, 0);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_EAST_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter);

            currentPos = new Tuple<int, int>(3, 0);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_EAST_COMMAND;
            expectedPos = new Tuple<int, int>(4, 0);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter + 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_SOUTH()
        {
            _testData.startingRobot.MoveCommandCounter = _testData.defaultCounter;
            Tuple<int, int> currentPos = new Tuple<int, int>(0, 0);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_SOUTH_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter);

            currentPos = new Tuple<int, int>(0, 1);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_SOUTH_COMMAND;
            expectedPos = new Tuple<int, int>(0, 0);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter + 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_WEST()
        {
            _testData.startingRobot.MoveCommandCounter = _testData.defaultCounter;
            Tuple<int, int> currentPos = new Tuple<int, int>(0, 1);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_WEST_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter);

            currentPos = new Tuple<int, int>(1, 1);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_WEST_COMMAND;
            expectedPos = new Tuple<int, int>(0,1);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, _testData.defaultCounter + 1);
        }
    }
}
