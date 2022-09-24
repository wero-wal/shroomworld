using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandlerPrototype
{
    internal class EnemyEventArgs : EventArgs
    {
        public string Type;
        public int AttackStrength;
    }
}
