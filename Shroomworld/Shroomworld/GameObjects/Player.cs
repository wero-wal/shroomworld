using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal class Player : IEntity
    {
        // ----- Enums -----
        // ----- Properties -----
        public IType Type => _type;
        public HealthData HealthData => _healthData;
        public static MovementData MovementData => _movementData;


        // ----- Fields -----
        public event Action<> PlacedOrRemovedTile;
        public event Action<> Moved;

        private readonly PlayerType _type;
        private readonly HealthData _healthData;
        private readonly MovementData _movementData;
        private readonly PowerUps _powerUps;
        private readonly InventoryItem[,] _inventory;
        private readonly List<Quest> _quests;
        private readonly Dictionary<string, int> _statistics;


        // ----- Constructors -----
        public Player(PlayerType type, HealthData healthData, MovementData movementData,
        PowerUp[] powerUps, InventoryItem[,] inventory, List<Quest> activeQuests, StatisticsDictionary statistics)
        {
            _type = type;
            _sprite = sprite;
            _healthData = healthData;
            _movementData = movementData;
            _powerUps = powerUps;
            _inventory = inventory;
            _quests = activeQuests ?? new List<Quest>();
            _statistics = statistics;
        }


        // ----- Methods -----
        public void Attack(out int attackStrength)
        {
            attackStrength = _attack.Strength;
        }
    }
}
