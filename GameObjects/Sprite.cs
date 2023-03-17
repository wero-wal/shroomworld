using System;
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
        _hitbox = new Rectangle(Point.Zero, new Point(texture.Width, texture.Height));
    }

    // ----- Methods -----
    public void SetPosition(Vector2 position) {
        UpdateHitbox(position);
        _position = position;
        //_position = Shroomworld.DisplayHandler.ClampToScreen(_hitbox);
    }
    private void UpdateHitbox(Vector2 position) {
        _hitbox.X = (int) position.X;
        _hitbox.Y = (int) position.Y;
    }
}