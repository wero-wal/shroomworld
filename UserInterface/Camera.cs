using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;

namespace Shroomworld;
public class Camera {

    // ----- Properties -----
    public Matrix Transform => _transform;

    // ----- Fields -----
    private Matrix _transform;
    private readonly float _scale;

    // ----- Constructors -----
    /// <summary>
    /// 
    /// </summary>
    /// <param name="scale"></param>
    public Camera(float scale) {
        _scale = scale;
    }

    // ----- Methods -----
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetPosition">The position on which to centre the camera in world scale.</param>
    /// <param name="centreOfScreen">Centre of the screen.</param>
    public void Follow(Vector2 targetPosition, Vector2 centreOfScreen) {
        Matrix position = Matrix.CreateTranslation(-targetPosition.X, -targetPosition.Y, 0);
        Matrix offset = Matrix.CreateTranslation(centreOfScreen.X, centreOfScreen.Y, 0);
        Matrix scale = Matrix.CreateScale(_scale);
        _transform = position * offset * scale;
    }
}
