using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Constants
{
    public class Message
    {
        public const string EnterTheCommandsOfRover = "Please enter commands of {0}. Rover by using \n'L' for Rotate Left\n'R' for Rotate Right\n'M' for Move Forward : ";
        public const string EnterTheRoverPositionAndDirection = "Please enter coordinates of {0}. Rover like\nX for x Axis\nY for Y Axis\nD for Direction\n'X Y D' : ";
        public const string EnterTheRoverCount = "Please enter the number of Rovers on the plateau : ";
        public const string RoverEndPosition = "End Position of {0}. rover is :{1} {2} {3}";
        public const string InvalidRoverCount = "Invalid Rover Count";
        public const string InvalidStartCoordinate = "Invalid Coordinates for Rover";
        public const string InvalidRoverPosition = "Invalid Rover Position";
        public const string RoverOutOfPlateau = "Rover is out of Plateau";
        public const string NoRover = "There is no rover on the plateau";
        public const string InvalidCommand = "Input has Invalid Command(s)";
        public const string UnexecutableCommand = "Command includes moves beyond plateau";
        public const string LoadedSpot = "There has been already loaded a rover at the same location";

        public const string EnterThePlateauDimesions = "Please enter the plateau dimensions formatted like 'X Y' : ";
        public const string InvalidPlateauDimensionCoordinate = "Invalid Coordinates for Plateau";
        public const string OutOfBoundriesPlateauCoordinates = "Rover Position is Beyond the Plateau";

        public const string RoversCrashed = "Rovers Crashed\n";

        public const string StartOver = "Do you want to start over again? \nY for yes\nN for no : ";
        public const string InvalidResponse = "Invalid Response";
    }
}
