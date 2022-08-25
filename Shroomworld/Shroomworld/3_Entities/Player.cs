using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Player : LivingBeing, IAggressive
    {
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        AttackInfo _attackInfo;
        List<PowerUp> _powerUps;
        InventoryItem[,] _inventory;
        Dictionary<string, int> _statistics;
        List<Quest> _activeQuests;

        // ---------- Constructors ----------


        // ---------- Methods ----------
        public void Attack(out byte attackStrength)
        {
            attackStrength = _attackInfo.Strength;
        }

    }
}
