using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class Sprite {

    // ----- Properties -----
    public Texture2D Texture => _texture;
    public Color Colour => _colour;
    public Vector2 Position => _position;
    public Rectangle Hitbox => _hitbox;

    // ----- Fields -----
    private readonly Texture2D _texture;
    private readonly Color _colour;
    private Rectangle _hitbox;
    private Vector2 _position;

    // ----- Constructors -----
    public Sprite(Texture2D texture) {
        _texture = texture;
        _hitbox = new Rectangle(Point.Zero, texture.Bounds.Size);
    }
    public Sprite(Texture2D texture, Vector2 position) {
        _texture = texture;
        _position = position;
        _hitbox = new Rectangle(position.ToPoint(), texture.Bounds.Size);
    }

    // ----- Methods -----
    public void SetPosition(Vector2 position) {
        _position = position;
        Shroomworld.DisplayHandler.ClampToScreen(ref _hitbox, ref _position);
    }
    public void ChangePosition(Vector2 changeBy) {
        _position += changeBy;
        Shroomworld.DisplayHandler.ClampToScreen(ref _hitbox, ref _position);
    }
    private void UpdateHitbox(Vector2 position) {
        _hitbox.X = (int) position.X;
        _hitbox.Y = (int) position.Y;
    }
}