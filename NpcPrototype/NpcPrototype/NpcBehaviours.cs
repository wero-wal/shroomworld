using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype
{
    internal static class NpcBehaviours
    {
        public static void RegenerateHealth(Npc source, NpcData npcData)
        {
            source.Data.IncreaseHealth();
        }


        // or...
        public static void RegenerateHealthF(ref NpcData npcData)
        {
            npcData.IncreaseHealth();
        }
    }
}
