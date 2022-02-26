using MarsRover.Constants.Enums;

namespace MarsRover.Constants.Models
{
    public class Rover
    {
        public Position StartPosition { get; set; }
        public Position EndPosition { get; set; }
        public Command[] Commands { get; set; }
    }
}
