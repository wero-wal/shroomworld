﻿using Shroomworld.Physics;

namespace Shroomworld;
public class Player : Entity {
    // ----- Enums -----
    // ----- Properties -----
    public Inventory Inventory => _inventory;

    // ----- Fields -----
    private readonly Inventory _inventory;
    //private readonly PowerUps _powerUps;
    //private readonly List<Quest> _quests;
    //private readonly Dictionary<string, int> _statistics;


    // ----- Constructors -----
    public Player(PlayerType type, Sprite sprite, EntityHealthData healthData, Inventory inventory
    /*,PowerUp[] powerUps, List<Quest> activeQuests, StatisticsDictionary statistics*/)
        : base(type, sprite, healthData, new Body(sprite, type.PhysicsData)) {
        _inventory = inventory;
        /*_powerUps = powerUps;
        _quests = activeQuests ?? new List<Quest>();
        _statistics = statistics;*/
    }


    // ----- Methods -----
    /*public void Attack(out int attackStrength) {
        attackStrength = _attack.Strength;
    }*/
}