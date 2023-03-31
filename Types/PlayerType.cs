using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shroomworld.Physics;

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
    public Player CreateNew(Vector2 position) {
        Sprite sprite = new Sprite(_texture, position);
        return new Player(_idData.Id, sprite, new EntityHealthData(_healthData), new Inventory(new InventoryItem[]
        { new InventoryItem(12, 1), new InventoryItem(13, 1), new InventoryItem(14, 1), new InventoryItem(15, 1) }),
        new Physics.Body(sprite, _physicsData));
    }
}