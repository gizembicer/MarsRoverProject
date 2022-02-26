using MarsRover.Business.Interfaces;
using MarsRover.Business.Services;
using MarsRover.Constants;
using MarsRover.Constants.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configure Services
            var serviceProvider = new ServiceCollection()
                  .AddSingleton<IRover, RoverService>()
                  .BuildServiceProvider();
            #endregion

            var _roverService = serviceProvider.GetService<IRover>();
            bool startOver = true;
            while (startOver)
            {
                try
                {
                    var plateau = SetInitialData.GetPlateauDimensions();

                    var roverEnded = _roverService.ExecuteCommand(plateau.Rovers, plateau.XAxis, plateau.YAxis);
                    Console.WriteLine(roverEnded);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(Message.StartOver);
                    startOver = ProcessResponse(Console.ReadLine().Trim().ToUpper());
                }
            }

            Console.ReadLine();
        }

        private static bool ProcessResponse(string reponse)
        {
            switch (reponse)
            {
                case "Y":
                    return true;
                case "N":
                    return false;
                default:
                    throw new Exception(Message.InvalidResponse);
            }
        }

        // aynı noktada iki rover olması

    }
}
