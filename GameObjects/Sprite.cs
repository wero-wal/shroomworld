using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class Sprite {

    // ----- Properties -----
    public Texture2D Texture => _texture;
    public Vector2 Position { get => _position; set => _position = value; }
    public Point Size => _size;
    public Rectangle Hitbox => _hitbox;

    // ----- Fields -----
    private readonly Texture2D _texture;
    private readonly Point _size;
    private Rectangle _hitbox;
    private Vector2 _position;

    // ----- Constructors -----
    public Sprite(Texture2D texture, IDisplayHandler displayHandler) {
        _texture = texture;
        _position = Vector2.Zero;
        _size = displayHandler.GetSizeInTiles(texture);
    }
    public Sprite(Texture2D texture, Vector2 position, IDisplayHandler displayHandler) {
        _texture = texture;
        _position = position;
        _size = displayHandler.GetSizeInTiles(texture);
    }

    // ----- Methods -----
    public static Rectangle GetHitbox(Vector2 position, Point size) {
		return new Rectangle(position.ToPoint(), (Vector2.Ceiling(position + size.ToVector2()) - Vector2.Floor(position)).ToPoint());
    }
    public void Update() {
        // Update the hitbox by flooring the position and ceiling the size.
		_hitbox = GetHitbox(_position, _size);
	}
}