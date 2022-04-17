using RovingRobot.Handlers;
using RovingRobot.Helpers;

Console.WriteLine("Hello, and welcome to Roving Robot!");
Console.WriteLine("Just setting the table, please wait...");
Console.WriteLine("Getting Rover's orders.");
FileLoader fileLoader = new FileLoader();
//Simple -
//List<string> commands = fileLoader.LoadCommandsFromFile("CommandSet_1.txt");

//Complex -
List<string> commands = fileLoader.LoadCommandsFromFile("CommandSet_2.txt");

CommandHandler commandHandler = new CommandHandler();
commandHandler.RunCommandList(commands);

Console.WriteLine("Waiting on input...");