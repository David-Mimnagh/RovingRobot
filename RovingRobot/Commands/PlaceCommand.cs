using RovingRobot.Helpers;
using RovingRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Commands
{
    [Command("PLACE")]
    internal class PlaceCommand : IBaseCommand
    {
        public Robot Robot { get; set; }
        public CommandValidator CommandValidator { get; set; }

        public PlaceCommand(Robot robot, CommandValidator commandValidator)
        {
            Robot = robot;
            CommandValidator = commandValidator;
        }

        public void ExecuteCommand(string? subCommand = null)
        {
            if (string.IsNullOrEmpty(subCommand) || !subCommand.Contains(','))
            {
                Console.WriteLine(@"Invalid Place command found.");
                Console.WriteLine(@"Please provide a following sub command for and primary place command");
                Console.WriteLine(@"in the following format X,Y,FACING DIRECTION(NORTH,EAST,SOUTH, OR WEST)");
                return;
            }

            List<string> subCommands = subCommand.Split(",").ToList();
            int xPlacePos = Convert.ToInt32(subCommands.ElementAt(0));
            int yPlacePos = Convert.ToInt32(subCommands.ElementAt(1));
            string newFacingDirection = subCommands.ElementAt(2);

            if (!CommandValidator.IsValidSubCommand(xPlacePos, yPlacePos, newFacingDirection))
            {
                Console.WriteLine("Invalid subcommand found following place command");
                Console.WriteLine("Please provide a positive X and Y position within the bounds of the board");
                Console.WriteLine("Please provide a valid face direction (NORTH, EAST, SOUTH, OR WEST)");
                return;
            }

            Robot.CurrentPosition = new Tuple<int, int>(xPlacePos, yPlacePos);
            Robot.FacingDirection = newFacingDirection;
            //If we are not on the board yet, set our starting pos
            if (Robot.StartingPosition.Item1 == -1)
            {
                Robot.StartingPosition = Robot.CurrentPosition;
            }
        }
    }
}
