using Microsoft.Xna.Framework;
namespace Shroomworld;
public class Vertices {

    // ----- Properties -----
    public const int Count = 4;
    public Vector2 TopLeft => _vertices[TopLeftIndex];
    public Vector2 TopRight => _vertices[TopRightIndex];
    public Vector2 BottomLeft => _vertices[BottomLeftIndex];
    public Vector2 BottomRight => _vertices[BottomRightIndex];
        
    public Vector2 this[int index] { get => _vertices[index]; }

    // ----- Fields -----
    private const int TopLeftIndex = 0;
    private const int TopRightIndex = 1;
    private const int BottomLeftIndex = 2;
    private const int BottomRightIndex = 3;
    private Vector2[] _vertices;

    // ----- Constructors -----
    public Vertices() {
        _vertices = new Vector2[Count];
    }
    public Vertices(Vector2 position, Vector2 size) {
        _vertices = new Vector2[Count];
        Update(position, size);
    }

    // ----- Methods -----
    public void Update(Vector2 position, Vector2 size) {
        _vertices[TopLeftIndex] = position;
        _vertices[TopRightIndex] = position + Vector2.UnitX * size.X;
        _vertices[BottomLeftIndex] = position + Vector2.UnitY * size.Y;
        _vertices[BottomRightIndex] = position + size;
    }
}
