using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiParser
{
    public class Variable
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }

        public Variable() { }
        public Variable(int offset, int length, string name)
        {
            Offset = offset;
            Length = length;
            Name = name;
        }
    }
}
