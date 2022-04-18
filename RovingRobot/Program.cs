using RovingRobot.Handlers;
using RovingRobot.Helpers;

Console.WriteLine("Hello, and welcome to Roving Robot!");
Console.WriteLine("Just setting the table, please wait...");
Console.WriteLine("Getting Rover's orders.");
FileLoader fileLoader = new FileLoader();
//Custom -
//Simple -
//List<string> commands = fileLoader.LoadCommandsFromFile("CommandSet_1.txt");
//Complex -
//List<string> commands = fileLoader.LoadCommandsFromFile("CommandSet_2.txt");
List<string> commands = fileLoader.LoadCommandsFromFile("CommandSet_3.txt");

//Exmaples
//List<string> commands = fileLoader.LoadCommandsFromFile("ExampleInput_1.txt");
//List<string> commands = fileLoader.LoadCommandsFromFile("ExampleInput_2.txt");
//List<string> commands = fileLoader.LoadCommandsFromFile("ExampleInput_3.txt");

CommandHandler commandHandler = new CommandHandler();
commandHandler.RunCommandList(commands);

Console.WriteLine("Waiting on input...");