using EnsureThat;
using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Mappers.Implementation
{
    public class RobotMapper : IRobotMapper
    {
        public Robot Map(IList<string> robotPosition)
        {
            EnsureArg.IsNotNull(robotPosition, nameof(robotPosition));
            EnsureArg.SizeIs(robotPosition, 3, nameof(robotPosition));

            return new Robot
            {
                XPosition = int.Parse(robotPosition[0]),
                YPosition = int.Parse(robotPosition[1]),
                CurrentDirection = (Direction)Enum.Parse(typeof(Direction), robotPosition[2])
            };
        }
    }
}
