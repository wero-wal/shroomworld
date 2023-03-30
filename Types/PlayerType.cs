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
    public Player CreateNew(Vector2 position, IDisplayHandler displayHandler) {
        return new Player(this, new Sprite(_texture, position, displayHandler), new EntityHealthData(_healthData), new Inventory());
    }
}