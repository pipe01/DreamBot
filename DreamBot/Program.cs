using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamBot
{
    class Program
    {
        static void Main(string[] args) => new DreamBot().Start().GetAwaiter().GetResult();
    }
}
