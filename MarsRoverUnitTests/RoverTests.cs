using MarsRover;
using MarsRover.Business.Services;
using MarsRover.Constants;
using MarsRover.Constants.Enums;
using MarsRover.Constants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace MarsRoverUnitTests
{
    public class RoverTests
    {
        [Fact]
        public void SettingNumberOfRovers_InputPositive2_ReturnsTheSame()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            var numberOfRovers = userInputValidator.NumberOfRovers("2");
            Assert.Equal<ulong>(2, numberOfRovers);
        }
        [Fact]
        public void SettingNumberOfRovers_InputNegative1_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                userInputValidator.NumberOfRovers("-1");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidRoverCount, ex.Message);
            }

        }
        [Fact]
        public void SettingNumberOfRovers_Input0_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                userInputValidator.NumberOfRovers("0");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.NoRover, ex.Message);
            }

        }
        [Fact]
        public void SettingFirstRoverPosition_Input12N_SuccessfulyCreated()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            var plateau = userInputValidator.PlateauDimensions("5 5");
            var roverPosition = userInputValidator.RoverPosition("1 2 N");
            var expectedPosition = new Position() { XAxis = 1, YAxis = 2, Direction = Direction.N };
            Assert.Equal(JsonSerializer.Serialize(expectedPosition), JsonSerializer.Serialize(roverPosition));
        }
        [Fact]
        public void SettingFirstRoverPosition_Input12D_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                var plateau = userInputValidator.PlateauDimensions("5 5");
                var rover = userInputValidator.RoverPosition("1 2 D");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidRoverPosition, ex.Message);

            }
        }
        [Fact]
        public void SettingFirstRoverPosition_Input12_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                var plateau = userInputValidator.PlateauDimensions("5 5");
                var rover = userInputValidator.RoverPosition("1 2");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidRoverPosition, ex.Message);

            }
        }
        [Fact]
        public void SettingFirstRoverPosition_Input36W_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                var plateau = userInputValidator.PlateauDimensions("5 5");
                var rover = userInputValidator.RoverPosition("3 6 W");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidRoverPosition, ex.Message);

            }
        }
        [Fact]
        public void SettingFirstRoverPosition_InputNegative13E_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                var plateau = userInputValidator.PlateauDimensions("5 5");
                var rover = userInputValidator.RoverPosition("-1 3 E");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidRoverPosition, ex.Message);

            }
        }
        [Fact]
        public void SetRoverCommand_InputLMLMLMLMM_ReturnsRightObject()
        {
            UserInputValidator userInputValidator = new UserInputValidator();

            var plateau = userInputValidator.PlateauDimensions("5 5");
            var roverPosition = userInputValidator.RoverPosition("1 2 N");
            var roverCommands = userInputValidator.RoverCommands("LMLMLMLMM");
            Rover rover = new Rover() { StartPosition = new Position(roverPosition), EndPosition = new Position(roverPosition), Commands = roverCommands };
            var expectedRover = new Rover()
            {
                StartPosition = new Position() { XAxis = 1, YAxis = 2, Direction = Direction.N },
                EndPosition = new Position() { XAxis = 1, YAxis = 2, Direction = Direction.N },
                Commands = new[] { Command.L, Command.M, Command.L, Command.M, Command.L, Command.M, Command.L, Command.M, Command.M }
            };
            Assert.Equal(JsonSerializer.Serialize(expectedRover), JsonSerializer.Serialize(rover));
        }
        [Fact]
        public void MoveRover_InputLMLMLMLMM_SuccessfulyMoved()
        {
            UserInputValidator userInputValidator = new UserInputValidator();

            var plateau = userInputValidator.PlateauDimensions("5 5");
            var roverPosition = userInputValidator.RoverPosition("1 2 N");
            var roverCommands = userInputValidator.RoverCommands("LMLMLMLMM");
            List<Rover> rovers = new List<Rover>() { new Rover() { StartPosition = new Position(roverPosition), EndPosition = new Position(roverPosition), Commands = roverCommands } };

            var roverService = new RoverService();
            var response = roverService.ExecuteCommand(rovers, plateau.XAxis, plateau.YAxis);
            var expectedRover = new Rover()
            {
                StartPosition = new Position() { XAxis = 1, YAxis = 2, Direction = Direction.N },
                EndPosition = new Position() { XAxis = 1, YAxis = 3, Direction = Direction.N },
                Commands = new[] { Command.L, Command.M, Command.L, Command.M, Command.L, Command.M, Command.L, Command.M, Command.M }
            };
            Assert.IsType<string>(response);
            Assert.Equal(JsonSerializer.Serialize(expectedRover), JsonSerializer.Serialize(rovers.First()));
        }
        [Fact]
        public void CrashedRovers_InputRRMMLMlM_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();

            var plateau = userInputValidator.PlateauDimensions("5 5");
            var roverPosition = userInputValidator.RoverPosition("1 2 N");
            var roverCommands = userInputValidator.RoverCommands("LMMLMLMLMM");
            List<Rover> rovers = new List<Rover>() { new Rover() { StartPosition = new Position(userInputValidator.RoverPosition("1 2 N")), EndPosition = new Position(roverPosition), Commands = userInputValidator.RoverCommands("LMLMLMLMM") },  new Rover() { StartPosition = new Position(userInputValidator.RoverPosition("3 3 E")), EndPosition = new Position(roverPosition), Commands = userInputValidator.RoverCommands("RRMMLMlM") } };

            var roverService = new RoverService();
            try
            {
                var response = roverService.ExecuteCommand(rovers, plateau.XAxis, plateau.YAxis);
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.RoversCrashed, ex.Message);

            }
        }
    }
}
