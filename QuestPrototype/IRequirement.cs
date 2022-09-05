using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal interface IRequirement
    {
        // ---------- Properties ----------
        public string Description { get; }
        public bool Completed { get; }

        // ---------- Methods ----------
        public void Update(Player player);
    }
}
