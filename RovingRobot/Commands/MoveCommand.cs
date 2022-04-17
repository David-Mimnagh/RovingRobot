using RovingRobot.Helpers;
using RovingRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Commands
{
    [Command("MOVE")]
    internal class MoveCommand : IBaseCommand
    {
        public Robot Robot { get; set; }
        public CommandValidator CommandValidator { get; set; }

        public MoveCommand(Robot robot, CommandValidator commandValidator)
        {
            Robot = robot;
            CommandValidator = commandValidator;
        }

        public void ExecuteCommand(string? subCommand = null)
        {
            if(!CommandValidator.IsValidPosition(Robot.CurrentPosition.Item1, Robot.CurrentPosition.Item2))
            {
                Console.WriteLine("Ignoring MOVE command, as Rover isn't in a valid position");
                return;
            }
            Robot.HandleMovement(); 
        }
    }
}
