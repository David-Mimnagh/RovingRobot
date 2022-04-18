using RovingRobot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RovingRobot.Models
{
    public class Robot
    {
        public Robot()
        {
            StartingPosition = new Tuple<int, int>(-1, -1); // Robot starts off the board;
            CurrentPosition = new Tuple<int, int>(-1, -1);
            FacingDirection = ProgramConstants.FACE_NORTH_COMMAND; // Will start facing north as not been told otherwise
            TableBoundryHits = 0;
            MoveCommandCounter = 0;
        }
        public Tuple<int, int> StartingPosition { get; set; }
        public Tuple<int,int> CurrentPosition { get; set; }
        public string FacingDirection { get; set; }
        public int TableBoundryHits { get; set; }
        public int MoveCommandCounter { get; set; }

        public bool isDangerousMove(int currentBoardAxisPos, bool isMaxBoardCheck)
        {
            int numToCheck = isMaxBoardCheck ? ProgramConstants.MAX_BOARD_WIDTH_HEIGHT - 1 : 0;
            if (currentBoardAxisPos == numToCheck)
            {
                Console.WriteLine("Beep Boop... This move looks dangerous.");
                Console.WriteLine("I think I'll stay where I am for now.");
                Console.WriteLine("(Movement will take Rover off the board)");
                TableBoundryHits++;
                return true;
            }
            return false;
        }
        public void OutputCurrentPosition()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Rover is currently facing {FacingDirection}");
            Console.WriteLine($"At position: X: {CurrentPosition.Item1}, Y: {CurrentPosition.Item2}");
            Console.WriteLine(Environment.NewLine);
        }

        public void HandleRotation(string rotationCommand)
        {
            int currentDirectionIndex = ProgramConstants.DIRECTIONS.IndexOf(FacingDirection);
            int directionOffset = rotationCommand == ProgramConstants.LEFT_COMMAND ? -1 : 1;

            if(directionOffset < 0 && currentDirectionIndex == 0)
            {
                FacingDirection = ProgramConstants.DIRECTIONS.Last();
                return;
            }
            if (directionOffset > 0 && currentDirectionIndex == ProgramConstants.DIRECTIONS.Count - 1)
            {
                FacingDirection = ProgramConstants.DIRECTIONS.First();
                return;
            }
            FacingDirection = ProgramConstants.DIRECTIONS[currentDirectionIndex + directionOffset];
        }

        public void HandleMovement()
        {
            /**
             * As We have a board of 5 x 5 and we're assuming 0,0 is the bottom left 
             * Then the root pos/starting pos is actually 1,5 in this model.
             */

            switch (FacingDirection)
            {
                case ProgramConstants.FACE_NORTH_COMMAND:
                    if(isDangerousMove(CurrentPosition.Item2, true))
                    {
                        return;
                    }

                    CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1, CurrentPosition.Item2 + 1);
                    break;
                case ProgramConstants.FACE_EAST_COMMAND:
                    if (isDangerousMove(CurrentPosition.Item1, true))
                    {
                        return;
                    }

                    CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1 + 1, CurrentPosition.Item2);
                    break;
                case ProgramConstants.FACE_SOUTH_COMMAND:
                    if (isDangerousMove(CurrentPosition.Item2, false))
                    {
                        return;
                    }

                    CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1, CurrentPosition.Item2 - 1);
                    break;
                case ProgramConstants.FACE_WEST_COMMAND:
                    if (isDangerousMove(CurrentPosition.Item1, false))
                    {
                        return;
                    }

                    CurrentPosition = new Tuple<int, int>(CurrentPosition.Item1 - 1, CurrentPosition.Item2);
                    break;
            }
            MoveCommandCounter++;
        }
    }
}
