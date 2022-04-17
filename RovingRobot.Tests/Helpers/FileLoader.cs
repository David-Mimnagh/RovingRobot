using System.Collections.Generic;
using Xunit;

namespace RovingRobot.Tests.Helpers
{
    public class FileLoader
    {
        private readonly RovingRobot.Helpers.FileLoader _fileLoader;
        private readonly Files.TestData _testData;

        public FileLoader()
        {
            _fileLoader = new RovingRobot.Helpers.FileLoader();
            _testData = new Files.TestData();
        }
        [Theory]
        [InlineData("bobthe.builder", false)]
        [InlineData("commands1.txt", true)]
        [InlineData("commands2.txts", false)]
        [InlineData("commands3.TXT", true)]
        [InlineData("commands4.TEXT", false)]
        public void PrepareCommand_ReturnsTheCorrectlyPreparedCommand(string fileName, bool expectedValidityValue)
        {
            var result = _fileLoader.IsValidFileExtension(fileName);

            Assert.Equal(result, expectedValidityValue);
        }
        [Fact]
        public void LoadCommandsFromFile_LoadsTheCommandsCorrectly()
        {
            var result = _fileLoader.LoadCommandsFromFile("TestCommandSet_1.txt");

            Assert.Equal(result, _testData.TestCommandSet_1CommandList);
        }
        [Fact]
        public void LoadCommandsFromFile_ReturnsAnEmptyListWhenFileIsNotFound()
        {
            var result = _fileLoader.LoadCommandsFromFile("TestCommandSet_1421.txt");

            Assert.Equal(result, new List<string>());
        }
    }
}
