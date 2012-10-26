using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyorDefence.Missiles;

namespace ConveyorDefence.Nodes.Strategies
{
    abstract class ProcessStrategy
    {
        public abstract void Process(ref List<Missile> missiles);
    }
}
