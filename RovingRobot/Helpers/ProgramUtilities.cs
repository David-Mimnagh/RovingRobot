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
        static internal IBaseCommand GetImplementingCommandClass(List<Type> allCommands, string command, Models.Robot robot, Helpers.CommandValidator commandValidator)
        {
            string commandToRun = allCommands.FirstOrDefault(c => c.GetCustomAttribute<RovingRobot.Helpers.CommandAttribute>().CommandType.Contains(command)).FullName;
            Type commandType = Type.GetType(commandToRun);
            return Activator.CreateInstance(commandType, robot, commandValidator) as IBaseCommand;
        }
    }
}
