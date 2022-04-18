using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Helpers
{
    public class FileLoader
    {
        public bool IsValidFileExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            return ProgramConstants.SUPPORTED_FILETYPES.Contains(fileExtension.ToLower());
        }
        public List<string> LoadPossibleInputFiles()
        {
            return Directory.GetFiles(@"Files/", "*.txt").ToList();

        }
        public List<string> LoadCommandsFromFile(string fileName)
        {
            List<string> commands = new List<string>();
            try 
            {
                return File.ReadLines(fileName).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {fileName}", ex.Message);
                return commands; // return the empty list.
            }
        }
    }
}
