using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Services
{
    public interface IProcessingService
    {
        string ProcessRobot(ref Grid grid, IList<string> robotPosition, string instructions);
    }
}
