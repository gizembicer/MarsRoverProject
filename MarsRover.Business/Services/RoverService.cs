using MarsRover.Business.Interfaces;
using MarsRover.Constants;
using MarsRover.Constants.Enums;
using MarsRover.Constants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Business.Services
{
    public class RoverService : IRover
    {
        public string ExecuteCommand(List<Rover> rovers, long maxXAxis, long maxYAxis)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int numberOfRover = 1;
            foreach (var rover in rovers)
            {
                var otherRovers = new List<Rover>(rovers);
                otherRovers.Remove(rover);
                Position endPosition = rover.EndPosition;
                foreach (var command in rover.Commands)
                {
                    switch (command)
                    {
                        case Command.L:
                            endPosition = RotateLeft(rover.EndPosition);
                            break;
                        case Command.R:
                            endPosition = RotateRight(rover.EndPosition);
                            break;
                        case Command.M:
                            endPosition = MoveForward(rover.EndPosition);
                            if (!ControlPlateauBoundries(endPosition, maxXAxis, maxYAxis))
                                throw new Exception(Message.UnexecutableCommand);
                            if (ControlCrachedOfRovers(otherRovers, endPosition))
                                throw new Exception(Message.RoversCrashed);
                            break;
                    }
                }

                rover.EndPosition = endPosition;

                stringBuilder.AppendLine(string.Format(Message.RoverEndPosition, numberOfRover, rover.EndPosition.XAxis, rover.EndPosition.YAxis, rover.EndPosition.Direction));
                numberOfRover++;
            }
            return stringBuilder.ToString();
        }

        private bool ControlCrachedOfRovers(List<Rover> rovers, Position endPosition)
        {
            if (rovers.Any(x => x.EndPosition.XAxis == endPosition.XAxis && x.EndPosition.YAxis == endPosition.YAxis))
                return true;
            else
                return false;
        }

        private bool ControlPlateauBoundries(Position endPosition, long maxXAxis, long maxYAxis)
        {
            if (endPosition.XAxis >= 0 && endPosition.YAxis >= 0 && endPosition.XAxis <= maxXAxis && endPosition.YAxis <= maxYAxis)
                return true;
            else
                return false;
        }
        private Position RotateRight(Position position)
        {
            switch (position.Direction)
            {
                case Direction.N: position.Direction = Direction.E; break;
                case Direction.E: position.Direction = Direction.S; break;
                case Direction.W: position.Direction = Direction.N; break;
                case Direction.S: position.Direction = Direction.W; break;
            }
            return position;
        }
        private Position RotateLeft(Position position)
        {
            switch (position.Direction)
            {
                case Direction.N: position.Direction = Direction.W; break;
                case Direction.E: position.Direction = Direction.N; break;
                case Direction.W: position.Direction = Direction.S; break;
                case Direction.S: position.Direction = Direction.E; break;
            }
            return position;
        }
        private Position MoveForward(Position position)
        {
            switch (position.Direction)
            {
                case Direction.N: position.YAxis = position.YAxis + 1; break;
                case Direction.E: position.XAxis = position.XAxis + 1; break;
                case Direction.W: position.XAxis = position.XAxis - 1; break;
                case Direction.S: position.YAxis = position.YAxis - 1; break;
            }
            return position;
        }
    }
}
