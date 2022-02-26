using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Constants.Models
{
    public class Plateau : Coordinates
    {
        public List<Rover> Rovers { get; set; } = new List<Rover>();

        public Plateau SetPlateauDimensions(Coordinates coordinates)
        {
            Plateau plateau = new Plateau();
            plateau.XAxis = coordinates.XAxis;
            plateau.YAxis = coordinates.YAxis;
            return plateau;
        }

    }
}
