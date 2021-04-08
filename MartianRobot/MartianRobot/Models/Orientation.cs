using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Models
{

    public class Orientation
    {
        public static readonly List<Direction> Directions = new List<Direction> { Direction.N, Direction.E, Direction.S, Direction.W} ;
    }
    public enum Direction
    {
        N = 0,
        S = 1,
        E = 2,
        W = 3
    }
}
