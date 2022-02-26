using MarsRover.Constants;
using MarsRover.Constants.Models;
using System;
using System.Linq;

namespace MarsRover
{
    public class SetInitialData
    {
        public static Plateau GetPlateauDimensions()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            Console.Write(Message.EnterThePlateauDimesions);
            var plateau = userInputValidator.PlateauDimensions(Console.ReadLine().Trim());
            Console.Write(Message.EnterTheRoverCount);
            var numberOfRovers = userInputValidator.NumberOfRovers(Console.ReadLine().Trim());
            for (uint i = 1; i <= numberOfRovers; i++)
            {
                Console.Write(string.Format(Message.EnterTheRoverPositionAndDirection, i));
                var position = userInputValidator.RoverPosition(Console.ReadLine().Trim());
                Console.Write(string.Format(Message.EnterTheCommandsOfRover, i));
                var commands = userInputValidator.RoverCommands(Console.ReadLine().Trim());
                Rover rover = new Rover()
                {
                    StartPosition = new Position(position),
                    EndPosition = new Position(position),
                    Commands = commands
                };
                if (!userInputValidator.CheckAvailabilityForLocationForNewRover(plateau.Rovers, rover))
                {
                    Console.Write(Message.LoadedSpot);
                    i--;
                }
                else
                    plateau.Rovers.Add(rover);

            }
            return plateau;
        }
    }
}
