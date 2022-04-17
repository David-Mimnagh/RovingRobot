using RovingRobot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Handlers
{
    internal class CommandHandler
    {
        Helpers.CommandValidator _commandValidator;
        Models.Robot RoverTheRovingRobot;
        List<Type>? allCommands;
        public CommandHandler()
        {
            _commandValidator = new Helpers.CommandValidator();
            RoverTheRovingRobot = new Models.Robot();
            allCommands = Helpers.ProgramUtilities.GetClassesWithCustomAttribute<RovingRobot.Helpers.CommandAttribute>();
        }

        void ExecuteCommand(string command)
        {
           string? subCommand = null;
           if(command == null) 
            {
                Console.WriteLine($"Invalid command. Ignoring");
                return; 
            }

            if (command.Contains(Helpers.ProgramConstants.PLACE_COMMAND))
            {
                subCommand = command.Substring(Helpers.ProgramConstants.PLACE_COMMAND.Length);
                command = command.Substring(0, command.IndexOf(subCommand));
            }
            if (command == Helpers.ProgramConstants.LEFT_COMMAND || command == Helpers.ProgramConstants.RIGHT_COMMAND)
            {
                subCommand = command;
            }
            IBaseCommand commandClass = Helpers.ProgramUtilities.GetImplementingCommandClass(allCommands, command, RoverTheRovingRobot, _commandValidator);
            commandClass.ExecuteCommand(subCommand);
        }
        internal void RunCommandList(List<string> commands)
        {
            if (!allCommands.Any())
            {
                Console.WriteLine("Trouble finding implementing classes of IBaseCommand. Exiting");
                return;
            }
            if (!_commandValidator.IsValidCommandList(commands))
            {
                Console.WriteLine("Invalid command list. Please make sure your command list contains a place command");
                return;
            }

            foreach (string command in commands)
            {
                Console.WriteLine("====================================================================================");
                Console.WriteLine($"\t\t ---- Processing new command: {command} ---- ");
                Console.WriteLine("====================================================================================");
                ExecuteCommand(command);
                RoverTheRovingRobot.OutputCurrentPosition();
            }

        }
    }
}
