using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Tests.Files
{
    internal class TestData
    {
        internal List<string> TestCommandSet_1CommandList = new List<string>()
        {
            {"PLACE 3 2 WEST"},
            {"MOVE"},
            {"MOVE"},
            {"MOVE"},
            {"RIGHT"},
            {"MOVE"},
            {"LEFT"},
            {"MOVE"},
            {"REPORT"}
        };

        internal List<string> ValidCommandList1 = new List<string>()
        {
            {"PLACE 3 2 WEST"},
            {"MOVE"},
            {"MOVE"},
            {"REPORT"}
        };
        internal List<string> ValidCommandList2 = new List<string>()
        {
            {"MOVE"},
            {"MOVE"},
            {"PLACE 3 2 WEST"},
            {"REPORT"}
        };
        internal List<string> InvalidCommandList1 = new List<string>()
        {
            {"MOVE"},
            {"MOVE"},
            {"REPORT"}
        };
        internal List<string> InvalidCommandList2 = new List<string>()
        {
            {"LEFT"},
            {"MOVE"},
            {"RIGHT"},
            {"MOVE"},
            {"REPORT"}
        };
        internal List<string> EmptyCommandList = new List<string>();

        internal RovingRobot.Models.Robot startingRobot = new RovingRobot.Models.Robot();
        internal static Tuple<int, int> startingPos1 = new Tuple<int, int>(2, 2);
    }
}
