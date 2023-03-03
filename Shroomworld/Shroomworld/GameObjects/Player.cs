using System;
using System.Collections.Generic;
using System.Microsoft.Xna.Framework;
using Shroomworld.Physics;

namespace Shroomworld;
public class Player : IEntity {
    // ----- Enums -----
    // ----- Properties -----
    public IType Type => _type;
    public EntityHealthData HealthData => _healthData;
    public Body Body => _body;


    // ----- Fields -----
    public event Action<> PlacedOrRemovedTile;
    public event Action<> Moved;

    private readonly PlayerType _type;
    private readonly Body _body;
    private readonly Sprite _sprite;
    private readonly EntityHealthData _healthData;
    //private readonly PowerUps _powerUps;
    private readonly InventoryItem[,] _inventory;
    //private readonly List<Quest> _quests;
    //private readonly Dictionary<string, int> _statistics;


    // ----- Constructors -----
    public Player(PlayerType type, EntityHealthData healthData,
    /*PowerUp[] powerUps, InventoryItem[,] inventory, List<Quest> activeQuests, StatisticsDictionary statistics*/)
    {
        _type = type;
        _sprite = type.GetSprite();
        _healthData = healthData;
        /*_powerUps = powerUps;
        _inventory = inventory;
        _quests = activeQuests ?? new List<Quest>();
        _statistics = statistics;*/
        _body = new Body(_sprite, type.PhysicsData);
    }


    // ----- Methods -----
    /*public void Attack(out int attackStrength)
    {
        attackStrength = _attack.Strength;
    }*/
}