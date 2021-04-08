using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Mappers
{
    public interface IGridMapper
    {
        Grid Map(IList<string> gridScale);
    }
}
