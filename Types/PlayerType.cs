using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class PlayerType : IType {

    // ----- Properties ----- 
    public IdData IdData => _idData;
    public Texture2D Texture => _texture;
    public HealthData HealthData => _healthData;
    public PhysicsData PhysicsData => _physicsData;


    // ----- Fields -----
    private readonly IdData _idData;
    private readonly Texture2D _texture;
    private readonly HealthData _healthData;
    private readonly PhysicsData _physicsData;


    // ----- Constructors -----
    public PlayerType(IdData idData, Texture2D texture, HealthData healthData, PhysicsData physicsData) {
        _idData = idData;
        _texture = texture;
        _healthData = healthData;
        _physicsData = physicsData;
    }

    // ----- Methods -----
    public Player CreateNew(Vector2? position = null, List<Quest> quests = null) {
        Sprite sprite = new Sprite(_texture, position ?? Vector2.Zero);
        return new Player(
            id: _idData.Id,
            sprite: sprite,
            healthData: new EntityHealthData(_healthData),
            inventory: new Inventory(GetDefaultItems()),
            body: new Physics.Body(sprite, _physicsData),
            activeQuests: quests
        );
    }
    private InventoryItem[] GetDefaultItems() {
        return new InventoryItem[] {
            new InventoryItem(ItemType.DefaultPickaxeId, 1),
            new InventoryItem(ItemType.DefaultAxeId, 1),
            new InventoryItem(ItemType.DefaultShovelId, 1),
            new InventoryItem(ItemType.DefaultSwordId, 1) };
    }
}