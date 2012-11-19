using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiParser
{
    public interface Parser<out T>
    {
        bool Verify(string expression);
        T Parse(string expression);
    }
}
