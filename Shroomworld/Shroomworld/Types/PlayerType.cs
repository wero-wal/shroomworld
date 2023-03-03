using System;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public class PlayerType : IType {
    // ----- Properties -----
    

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
    public Player CreatePlayer() {
        return new Player(this, new Sprite(_texture), new EntityHealthData(_healthData));
    }
}