using RovingRobot.Helpers;
using RovingRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Commands
{
    [Command("REPORT")]
    public class ReportCommand : IBaseCommand
    {
        public Robot Robot { get; set; }
        public CommandValidator CommandValidator { get; set; }

        public ReportCommand(Robot robot, CommandValidator commandValidator)
        {
            Robot = robot;
            CommandValidator = commandValidator;
        }

        public void ExecuteCommand(string? subCommand = null)
        {
            Console.WriteLine("Executing Rover Report...");
            Console.WriteLine("Analyzing...");
            Console.WriteLine($"Rover is has moved a total of {Robot.MoveCommandCounter} times");
            Console.WriteLine($"Rover's safety system engaged a total of {Robot.TableBoundryHits} times to stop him falling.");
        }
    }
}
