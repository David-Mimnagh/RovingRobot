using RovingRobot.Handlers;
using RovingRobot.Helpers;

FileLoader fileLoader = new FileLoader();
Console.WriteLine("Hello, and welcome to Roving Robot!");
Console.WriteLine("Just setting the table, please wait...");
List<string> commandInputFiles = fileLoader.LoadPossibleInputFiles();
List<string> commands = new List<string>();
Console.WriteLine($"Good news, I found {commandInputFiles.Count} files");
if (commandInputFiles.Count == 0)
{
    Console.WriteLine("No .txt files found within the Files directory");
    Console.WriteLine("Waiting on input...");
    return;
}
int fileIndexChoice = 0;
Console.WriteLine(Environment.NewLine);
Console.WriteLine("Please select the command set you would like to run:");
Console.WriteLine(Environment.NewLine);
for (int i = 1; i <= commandInputFiles.Count; i++)
{
    Console.WriteLine($"\t {i}: {commandInputFiles.ElementAt(i-1)}");
}

Console.WriteLine(Environment.NewLine);
while (fileIndexChoice <= 0 || fileIndexChoice > commandInputFiles.Count)
{
    Console.WriteLine("Please enter a number from the corresponding options above.");
    Console.Write("File Number Choice: ");
    string? choice = Console.ReadLine();
    bool isNumber = int.TryParse(choice, out fileIndexChoice);
    if (!isNumber)
    {
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Please enter a number.");
        Console.WriteLine(Environment.NewLine);
        continue;
    }
    Console.WriteLine(Environment.NewLine);
}
string selectedFileName = commandInputFiles.ElementAt(fileIndexChoice - 1);
Console.WriteLine($"Thanks. Getting Rover's orders from: {selectedFileName}.");

commands = fileLoader.LoadCommandsFromFile(selectedFileName);

CommandHandler commandHandler = new CommandHandler();
commandHandler.RunCommandList(commands);

Console.WriteLine(Environment.NewLine);
Console.WriteLine("Press any key to exit...");
Console.ReadLine();
Console.WriteLine(Environment.NewLine);