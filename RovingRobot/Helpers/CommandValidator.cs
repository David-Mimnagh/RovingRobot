using System.Text.RegularExpressions;

namespace RovingRobot.Helpers
{
    public class CommandValidator
    {
        public string PrepareCommand(string command)
        {
            //remove any possible spaces and uppercase the command to be safe.
            return Regex.Replace(command.ToUpper(), @"\s+", "");
        }
        public bool IsValidPosition(int x, int y)
        {   bool isValidX = x >= 0 && x < ProgramConstants.MAX_BOARD_WIDTH_HEIGHT; 
            bool isValidY = y >= 0 && y < ProgramConstants.MAX_BOARD_WIDTH_HEIGHT;
            return (isValidX && isValidY);
        }
        public bool IsValidRotationCommand(string rotation)
        {
            return (rotation != null && ProgramConstants.ROTATIIONS.Contains(rotation));
        }

        public bool IsValidDirectionCommand(string direction)
        {
            return (direction != null && ProgramConstants.DIRECTIONS.Contains(direction));
        }

        public bool IsValidPrimaryCommand(string primaryCommand)
        {
            return (primaryCommand != null && ProgramConstants.PRIMARY_COMMANDS.Contains(primaryCommand));
        }
        public bool IsValidSubCommand(int x, int y, string direction)
        {
            return (IsValidPosition(x,y) && IsValidDirectionCommand(direction));
        }
        public bool IsValidCommandList(List<string> commandList)
        {
            return (commandList != null && commandList.Count > 0 && commandList.Any(command => command.Contains(ProgramConstants.PLACE_COMMAND)));
        }
    }
}
