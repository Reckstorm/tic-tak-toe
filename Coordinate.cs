using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classwork_19._11
{
    internal class Coordinate
    {
        public int Num { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public override string ToString()
        {
            return $"{Num}";
        }
    }
}
