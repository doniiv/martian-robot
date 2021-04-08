using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Mappers
{
    public interface IInstructionMapper
    {
        Instruction Map(string instructions);
    }
}
