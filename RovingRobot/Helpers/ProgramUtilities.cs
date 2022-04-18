using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RovingRobot.Commands;
namespace RovingRobot.Helpers
{
    internal class ProgramUtilities
    {
        static internal List<Type> GetClassesWithCustomAttribute<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes()
                    .Where(t => t.IsDefined(typeof(T))))
                    .ToList();
        }
        static internal IBaseCommand? GetImplementingCommandClass(List<Type> allCommands, string command, Models.Robot robot, Helpers.CommandValidator commandValidator)
        {
            if (allCommands == null || !allCommands.Any())
            {
                Console.WriteLine("Trouble finding implementing classes of IBaseCommand. Exiting");
                return null;
            }
            string? commandToRun = allCommands.First(c => c.GetCustomAttribute<CommandAttribute>()!.CommandType.Contains(command)).FullName;
            if (commandToRun == null) return null;
            Type? commandType = Type.GetType(commandToRun);
            if (commandType == null) return null;
            return Activator.CreateInstance(commandType, robot, commandValidator) as IBaseCommand;
        }
    }
}
