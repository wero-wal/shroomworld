using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    internal class BiomeType : Type
    {
        private int
            _tree_coverage,
            _chest_chance,
            _npc_spawn_rate,
            _enemy_spawn_rate;
        private List<int> _tiles;

        public BiomeType(string fromFile)
        {
        }
    }
}
