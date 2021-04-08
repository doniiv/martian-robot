using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Models
{
    public class Robot
    {
        public Direction CurrentDirection { get; set; }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public bool IsLost { get; set; }
    }
}
