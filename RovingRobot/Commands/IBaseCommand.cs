using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Commands
{
    internal interface IBaseCommand
    {
        public Models.Robot Robot { get; set; }

        public Helpers.CommandValidator CommandValidator { get; set; }
        public void ExecuteCommand(string? subCommand = null);
    }
}
