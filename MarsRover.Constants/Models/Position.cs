using MarsRover.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Constants.Models
{
    public class Position : Coordinates
    {
        public Position()
        {

        }
        public Position(Position position)
        {
            XAxis = position.XAxis;
            YAxis = position.YAxis;
            Direction = position.Direction;
        }
        public Direction Direction { get; set; }
    }
}
