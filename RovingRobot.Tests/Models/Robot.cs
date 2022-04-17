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
            Assert.Equal(newRobot.FacingDirection, RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND);
            Assert.Equal(newRobot.TableBoundryHits, 0);
            Assert.Equal(newRobot.MoveCommandCounter, 0);
            Assert.Equal(newRobot.StartingPosition, startingPosition);
            Assert.Equal(newRobot.CurrentPosition, startingPosition);
        }

        [Theory]
        [InlineData(1, false, true)]
        [InlineData(5, true, true)]
        [InlineData(1, true, false)]
        [InlineData(5, false, false)]
        [InlineData(3, false, false)]
        [InlineData(3, true, false)]
        public void ShouldCorrectlyIdentifyADangerousMoveAndIncrementTheTableBoundryHitsCounter(int currentBoardAxisPos, bool isMaxBoardCheck, bool expectedResponse)
        {
            bool response = _testData.startingRobot.isDangerousMove(currentBoardAxisPos, isMaxBoardCheck);

            Assert.Equal(response, expectedResponse);
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
            _testData.startingRobot.MoveCommandCounter = 0;
            Tuple<int, int> currentPos = new Tuple<int, int>(1, 1);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 0);

            currentPos = new Tuple<int, int>(1, 2);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_NORTH_COMMAND;
            expectedPos = new Tuple<int, int>(1, 1);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_EAST()
        {
            _testData.startingRobot.MoveCommandCounter = 0;
            Tuple<int, int> currentPos = new Tuple<int, int>(5, 1);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_EAST_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 0);

            currentPos = new Tuple<int, int>(4, 1);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_EAST_COMMAND;
            expectedPos = new Tuple<int, int>(5, 1);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_SOUTH()
        {
            _testData.startingRobot.MoveCommandCounter = 0;
            Tuple<int, int> currentPos = new Tuple<int, int>(1, 5);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_SOUTH_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 0);

            currentPos = new Tuple<int, int>(1, 4);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_SOUTH_COMMAND;
            expectedPos = new Tuple<int, int>(1, 5);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 1);
        }

        [Fact]
        public void ShouldHandleMovementCorrectlyBasedOnTheCurrentDirectionAndPosition_WEST()
        {
            _testData.startingRobot.MoveCommandCounter = 0;
            Tuple<int, int> currentPos = new Tuple<int, int>(1, 2);
            string currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_WEST_COMMAND;
            Tuple<int, int> expectedPos = currentPos;
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //Dangerous movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 0);

            currentPos = new Tuple<int, int>(2, 2);
            currentFacingDirection = RovingRobot.Helpers.ProgramConstants.FACE_WEST_COMMAND;
            expectedPos = new Tuple<int, int>(1, 2);
            _testData.startingRobot.FacingDirection = currentFacingDirection;
            _testData.startingRobot.CurrentPosition = currentPos;
            _testData.startingRobot.HandleMovement();
            //valid movement
            Assert.Equal(_testData.startingRobot.CurrentPosition, expectedPos);
            Assert.Equal(_testData.startingRobot.MoveCommandCounter, 1);
        }
    }
}
