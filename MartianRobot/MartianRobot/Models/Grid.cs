using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Models
{
    public class Grid
    {
        public int XScale { get; set; }
        public int YScale { get; set; }
        public IList<int[]> Scents = new List<int[]>();
    }
}
