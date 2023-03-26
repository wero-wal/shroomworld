using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class Sprite {

    // ----- Properties -----
    public Texture2D Texture => _texture;
    public Vector2 Position { get => _position; set => _position = value; }
    public Point Size => _size;

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
		return new Rectangle(position.ToPoint(), new Point((int)Math.Ceiling(position.X + size.X) - (int)position.X, (int)Math.Ceiling(position.Y + size.Y) - (int)position.Y));
    }
    public void Update() {
        // Update the hitbox by flooring the position and ceiling the size.
		_hitbox = new Rectangle(_position.ToPoint(), new Point((int)Math.Ceiling(_position.X + _size.X) - (int)_position.X, (int)Math.Ceiling(_position.Y + _size.Y) - (int)_position.Y));
	}
    public Rectangle GetHitbox(Vector2 position) {
		return new Rectangle(position.ToPoint(), new Point((int)Math.Ceiling(position.X + _size.X) - (int)position.X, (int)Math.Ceiling(position.Y + _size.Y) - (int)position.Y));
    }
}