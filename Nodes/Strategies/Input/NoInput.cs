using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyorDefence.Missiles;

namespace ConveyorDefence.Nodes.Strategies.Input
{
    class NoInput:InputStrategy
    {
        public override void Input(ref Missile missile, ref List<Missile> missiles){}
    }
}
