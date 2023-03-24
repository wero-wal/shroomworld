using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class Sprite {

    // ----- Properties -----
    public Texture2D Texture => _texture;
    public Vector2 Position => _position;
    public Rectangle Hitbox => _hitbox;

    // ----- Fields -----
    private delegate void Clamper(ref Rectangle hitbox, ref Vector2 position) ;
    private static readonly Clamper _clampToWorld = Shroomworld.DisplayHandler.ClampToWorld;

    private readonly Texture2D _texture;
    private Rectangle _hitbox;
    private Vector2 _position;

    // ----- Constructors -----
    public Sprite(Texture2D texture) {
        _texture = texture;
        _position = Vector2.Zero;
        _hitbox = CalculateHitbox(_position);
    }
    public Sprite(Texture2D texture, Vector2 position) {
        _texture = texture;
        _position = position;
        _hitbox = CalculateHitbox(position);
    }

    // ----- Methods -----
    public void SetPosition(Vector2 position) {
        _position = position;
        _clampToWorld(ref _hitbox, ref _position);
    }
    public void ChangePosition(Vector2 changeBy) {
        _position += changeBy;
        _clampToWorld(ref _hitbox, ref _position);
    }
    public Rectangle SimulateChangePosition(Vector2 changeBy) {
        Vector2 newPosition = _position + changeBy;
        Rectangle newHitbox = CalculateHitbox(newPosition);
        _clampToWorld(ref newHitbox, ref newPosition);
        return newHitbox;
    }
    private Rectangle CalculateHitbox(Vector2 position) {
        return new Rectangle(position.ToPoint(), _texture.Bounds.Size);
    }
}