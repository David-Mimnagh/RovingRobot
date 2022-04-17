using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Helpers
{
    public class ProgramConstants
    {
        public static readonly int MAX_BOARD_WIDTH_HEIGHT = 5;

        public static readonly IList<String> SUPPORTED_FILETYPES = new ReadOnlyCollection<string>
        (new List<String> {
         ".txt"});

        #region Commands

        public const string PLACE_COMMAND = "PLACE";
        public const string MOVE_COMMAND = "MOVE";
        public const string LEFT_COMMAND = "LEFT";
        public const string RIGHT_COMMAND = "RIGHT";
        public const string REPORT_COMMAND = "REPORT";

        public const string FACE_NORTH_COMMAND = "NORTH";
        public const string FACE_EAST_COMMAND = "EAST";
        public const string FACE_SOUTH_COMMAND = "SOUTH";
        public const string FACE_WEST_COMMAND = "WEST";

        public static readonly IList<String> ROTATIIONS = new ReadOnlyCollection<string>
        (new List<String> {
         LEFT_COMMAND,
         RIGHT_COMMAND});

        public static readonly IList<String> DIRECTIONS = new ReadOnlyCollection<string>
        (new List<String> {
         FACE_NORTH_COMMAND,
         FACE_EAST_COMMAND,
         FACE_SOUTH_COMMAND,
         FACE_WEST_COMMAND });

        public static readonly IList<String> PRIMARY_COMMANDS = new ReadOnlyCollection<string>
        (new List<String> {
        PLACE_COMMAND,
         MOVE_COMMAND,
         LEFT_COMMAND,
         RIGHT_COMMAND,
         REPORT_COMMAND });

        #endregion
    }
}
