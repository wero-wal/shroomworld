using Microsoft.Xna.Framework;

namespace Shroomworld;
public class Camera {

    // ----- Properties -----
    public Matrix Transform => _transform;

    // ----- Fields -----
    private Matrix _transform;

    // ----- Constructors -----


    // ----- Methods -----
    public void Follow(Vector2 targetPosition) {
        Matrix position = Matrix.CreateTranslation(-targetPosition.X, -targetPosition.Y, 0);
        Matrix offset = Matrix.CreateTranslation(Shroomworld.DisplayHandler.CentreOfScreen.X, Shroomworld.DisplayHandler.CentreOfScreen.Y, 0);
        _transform = position * offset;
    }
}
