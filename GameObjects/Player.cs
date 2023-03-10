using Shroomworld.Physics;

namespace Shroomworld;
public class Player : Entity {
    // ----- Enums -----
    // ----- Properties -----

    // ----- Fields -----
    //private readonly PowerUps _powerUps;
    //private readonly InventoryItem[,] _inventory;
    //private readonly List<Quest> _quests;
    //private readonly Dictionary<string, int> _statistics;


    // ----- Constructors -----
    public Player(PlayerType type, Sprite sprite, EntityHealthData healthData/*,
    PowerUp[] powerUps, InventoryItem[,] inventory, List<Quest> activeQuests, StatisticsDictionary statistics*/)
        : base(type, sprite, healthData, new Body(sprite, type.PhysicsData)) {
        /*_powerUps = powerUps;
        _inventory = inventory;
        _quests = activeQuests ?? new List<Quest>();
        _statistics = statistics;*/
    }


    // ----- Methods -----
    /*public void Attack(out int attackStrength) {
        attackStrength = _attack.Strength;
    }*/
}