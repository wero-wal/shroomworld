using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype
{
    internal class Npc
    {
        internal NpcData Data { get => _data; set => _data = value; }


        public event EventHandler<NpcData> Behaviours;

        private NpcData _data;


        public Npc()
        {
            Behaviours += NpcBehaviours.RegenerateHealth;
        }


        public void DoBehaviours()
        {
            Behaviours?.Invoke(this, _data);
        }
    }
}
