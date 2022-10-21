using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype2
{
    internal class Game
    {
       private static Npc NewNpc(bool friendly)
        {
            Npc npc = new Npc();
            if (friendly)
            {
                npc.Updated += NpcBehaviour.RegenerateHealth;
            }
            npc.Updated += NpcBehaviour.Move;

            return npc;
        }
    }
}
