using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class Sprite {

    // ----- Properties -----
    public Texture2D Texture => _texture;
    public Color Colour => _colour;
    public Vector2 Position => _position;
    public Vector2 Size => _size;
    public Vertices Vertices => _vertices;

    // ----- Fields -----
    private static Action<Texture2D, Vector2, Color> _draw;

    private readonly Texture2D _texture;
    private readonly Vector2 _size;
    private readonly Color _colour;
    private readonly Vertices _vertices;
    private Vector2 _position;

    // ----- Constructors -----
    public Sprite(Texture2D texture) {
        _texture = texture;
        _size = new Vector2(_texture.Width, _texture.Height);
    }

    // ----- Methods -----
    public static void InjectDependencies(Action<Texture2D, Vector2, Color> drawFunction) {
        _draw = drawFunction;
    }

    public void UpdateVertices() {
        _vertices.Update(_position, _size);
    }
    public void Draw() {
        _draw(_texture, _position, _colour);
    }
    public void Draw(Vector2 position) {
        _draw(_texture, position, _colour);
    }
    public void Draw(Color colour) {
        _draw(_texture, _position, colour);
    }
    public void SetPosition(Vector2 position) {
        _position.X = Math.Clamp(position.X, Shroomworld.TopLeftOfScreen.X, Shroomworld.BottomRightOfScreen.X);
        _position.Y = Math.Clamp(position.Y, Shroomworld.TopLeftOfScreen.Y, Shroomworld.BottomRightOfScreen.Y);
    }
}
