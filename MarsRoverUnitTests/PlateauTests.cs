using MarsRover;
using MarsRover.Constants;
using MarsRover.Constants.Models;
using System;
using Xunit;

namespace MarsRoverUnitTests
{
    public class PlateauTests
    {
        [Fact]
        public void SettingDimensionsOfPlateau_InputPositive5_ReturnsTheSame()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            var plateau = userInputValidator.PlateauDimensions("5 5");
            var expectedPlateau = new Plateau() { XAxis = 5, YAxis = 5 };
            Object.Equals(expectedPlateau, plateau);
        }
        [Fact]
        public void SettingDimensionsOfPlateau_InputNegative1_ThrowsException()
        {
            UserInputValidator userInputValidator = new UserInputValidator();
            try
            {
                userInputValidator.PlateauDimensions("-1 5");
            }
            catch (Exception ex)
            {
                Assert.Contains(Message.InvalidPlateauDimensionCoordinate, ex.Message);
            }
            
        }
    }
}
