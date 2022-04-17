using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string commandType)
        {
            this.CommandType = commandType;
        }
        public virtual string CommandType { get; }

    }
}
