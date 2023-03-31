using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shroomworld.Physics;

namespace Shroomworld;
public class Player : Entity {
    // ----- Enums -----
    // ----- Properties -----
    public Inventory Inventory => _inventory;
    public List<Quest> Quests => _quests;

    // ----- Fields -----
    private readonly Inventory _inventory;
    private readonly List<Quest> _quests;
    //private readonly PowerUps _powerUps;
    //private readonly Dictionary<string, int> _statistics;


    // ----- Constructors -----
    public Player(int id, Sprite sprite, EntityHealthData healthData, Inventory inventory, Body body,
    List<Quest> activeQuests/*,PowerUp[] powerUps, StatisticsDictionary statistics*/)
        : base(id, sprite, healthData, body) {
        _inventory = inventory;
        _quests = activeQuests ?? new List<Quest>();
        /*_powerUps = powerUps;
        _statistics = statistics;*/
    }


    // ----- Methods -----
    public void Update(PlayerUpdateArgs args) {
        base.Update(args);
        _quests.ForEach(quest => quest.Update(_inventory));
        _sprite.Position = args.ClampToWorld(_sprite.Position, _sprite.Size);
    }
    /*public void Attack(out int attackStrength) {
        attackStrength = _attack.Strength;
    }*/
}
public class PlayerUpdateArgs : UpdateArgs {

    // ----- Fields -----
    public delegate Vector2 Clamper(Vector2 position, Point size);
    public readonly Clamper ClampToWorld;    

    // ----- Constructors -----
	public PlayerUpdateArgs(Clamper clampToWorld) {
		ClampToWorld = clampToWorld;
	}
}