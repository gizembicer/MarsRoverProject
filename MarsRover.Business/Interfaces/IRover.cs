using MarsRover.Constants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Business.Interfaces
{
    public interface IRover
    {
        string ExecuteCommand(List<Rover> rovers, long maxXAxis, long maxYAxis);
    }
}
