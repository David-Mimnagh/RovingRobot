using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Tests.Files
{
    internal class TestData
    {
        internal int defaultCounter = 0;
        internal List<string> TestCommandSet_1CommandList = new List<string>()
        {
            {"PLACE 3,2,WEST"},
            {"MOVE"},
            {"MOVE"},
            {"MOVE"},
            {"RIGHT"},
            {"MOVE"},
            {"LEFT"},
            {"MOVE"},
            {"REPORT"}
        };
        public RovingRobot.Models.Robot RobotAfterCommandSet1 = new RovingRobot.Models.Robot()
        {
            CurrentPosition = new Tuple<int, int>(0, 3),
            FacingDirection = "WEST",
            MoveCommandCounter = 4,
            TableBoundryHits = 1,
            StartingPosition = new Tuple<int, int>(3,2)
        };
        internal List<string> TestCommandSet_2CommandList = new List<string>()
        {
            {"MOVE"},
            {"MOVE"},
            {"RIGHT"},
            {"MOVE"},
            {"PLACE 1,5,EAST"},
            {"MOVE"},
            {"LEFT"},
            {"MOVE"},
            {"MOVE"},
            {"MOVE"},
            {"LEFT"},
            {"PLACE 6,1,NORTH"},
            {"PLACE 4,4,NORTH"},
            {"MOVE"},
            {"LEFT"},
            {"REPORT"}
        };
        public RovingRobot.Models.Robot RobotAfterCommandSet2 = new RovingRobot.Models.Robot()
        {
            CurrentPosition = new Tuple<int, int>(4,4),
            FacingDirection = "WEST",
            MoveCommandCounter = 0,
            TableBoundryHits = 1,
            StartingPosition = new Tuple<int, int>(4, 4)
        };
        internal List<string> TestCommandSet_3CommandList = new List<string>()
        {
            {"PLACE 0,0,EAST"},
            {"MOVE"},
            {"MOVE"},
            {"LEFT"},
            {"MOVE"},
            {"PLACE 3,3,EAST"},
            {"MOVE"},
            {"LEFT"},
            {"MOVE"},
            {"MOVE"},
            {"MOVE"},
            {"LEFT"},
            {"PLACE 6,1,NORTH"},
            {"PLACE 0,0,NORTH"},
            {"MOVE"},
            {"RIGHT"},
            {"MOVE"},
            {"MOVE"},
            {"REPORT"}
        };
        public RovingRobot.Models.Robot RobotAfterCommandSet3 = new RovingRobot.Models.Robot()
        {
            CurrentPosition = new Tuple<int, int>(2, 1),
            FacingDirection = "EAST",
            MoveCommandCounter = 8,
            TableBoundryHits = 2,
            StartingPosition = new Tuple<int, int>(0, 0)
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
