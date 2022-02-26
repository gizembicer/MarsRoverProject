using MarsRover.Constants;
using MarsRover.Constants.Enums;
using MarsRover.Constants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class UserInputValidator
    {
        private readonly char splitParameter = ' ';
        private Plateau _Plateau;
        public Plateau PlateauDimensions(string plateauDimensionsInput)
        {
            var splittedPlateauDimension = plateauDimensionsInput.Split(splitParameter);
            if (splittedPlateauDimension.Length != 2 || !splittedPlateauDimension[0].All(char.IsDigit) || !splittedPlateauDimension[1].All(char.IsDigit))
                throw new Exception(Message.InvalidPlateauDimensionCoordinate);
            else
            {
                long xAxis, yAxis;
                Int64.TryParse(splittedPlateauDimension[0], out xAxis);
                Int64.TryParse(splittedPlateauDimension[1], out yAxis);

                if (xAxis <= 0 || yAxis <= 0)
                    throw new Exception(Message.InvalidPlateauDimensionCoordinate);

                _Plateau = new Plateau();
                _Plateau.XAxis = xAxis;
                _Plateau.YAxis = yAxis;
                return _Plateau;
            }
        }

        public bool CheckAvailabilityForLocationForNewRover(List<Rover> rovers, Rover rover)
        {
            if (rovers.Where(x => x.StartPosition.XAxis == rover.StartPosition.XAxis && x.StartPosition.YAxis == rover.StartPosition.YAxis).Any())
                return false;
            else
                return true;
        }

        public Command[] RoverCommands(string inputCommands)
        {
            var stringCommandArray = inputCommands.ToUpper();
            if (stringCommandArray.Where(x => !Enum.TryParse(typeof(Command), x.ToString(), out _)).Any())
                throw new Exception(Message.InvalidCommand);
            return stringCommandArray.Select(s => s.ToString()).Select(x => (Command)Enum.Parse(typeof(Command), x)).ToArray();
        }

        public Position RoverPosition(string inputRoverPosition)
        {
            var splittedRoverPosition = inputRoverPosition.Split(splitParameter);
            long xAxis, yAxis;
            Direction direction;
            if (splittedRoverPosition.Length == 3 && splittedRoverPosition[0].All(char.IsDigit) && splittedRoverPosition[1].All(char.IsDigit) && splittedRoverPosition[2].All(char.IsLetter))
            {
                if (Enum.IsDefined(typeof(Direction), splittedRoverPosition[2].ToUpper()) && Int64.TryParse(splittedRoverPosition[1], out yAxis) && Int64.TryParse(splittedRoverPosition[0], out xAxis))
                {
                    if (xAxis >= 0 && yAxis >= 0)
                    {
                        if (xAxis >= _Plateau.XAxis && yAxis >= _Plateau.YAxis)
                            throw new Exception(Message.OutOfBoundriesPlateauCoordinates);
                    }
                    else
                        throw new Exception(Message.InvalidStartCoordinate);

                    direction = (Direction)Enum.Parse(typeof(Direction), splittedRoverPosition[2].ToUpper());
                }
                else
                    throw new Exception(Message.InvalidRoverPosition);

            }
            else
                throw new Exception(Message.InvalidRoverPosition);

            return new Position()
            {
                XAxis = xAxis,
                YAxis = yAxis,
                Direction = direction
            };
        }

        public ulong NumberOfRovers(string numberOfRoversInput)
        {
            ulong numberofRovers;
            if (!UInt64.TryParse(numberOfRoversInput, out numberofRovers))
                throw new Exception(Message.InvalidRoverCount);
            else if (numberofRovers == 0)
                throw new Exception(Message.NoRover);
            return numberofRovers;
        }
    }
}
