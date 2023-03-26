using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;

namespace Shroomworld;
public class Camera {

    // ----- Properties -----
    public Matrix Transform => _transform;

    // ----- Fields -----
    private Matrix _transform;
    private readonly float _scale;
    private readonly Vector2 _centreOfScreen;

    // ----- Constructors -----
    /// <summary>
    /// 
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="centreOfScreen">Centre of the screen in world scale.</param>
    public Camera(float scale, ref Vector2 centreOfScreen) {
        _scale = scale;
        _centreOfScreen = centreOfScreen;
    }

    // ----- Methods -----
    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetPosition">The position on which to centre the camera in world scale.</param>
    public void Follow(Vector2 targetPosition) {
        Matrix position = Matrix.CreateTranslation(-targetPosition.X, -targetPosition.Y, 0);
        Matrix offset = Matrix.CreateTranslation(_centreOfScreen.X, _centreOfScreen.Y, 0);
        Matrix scale = Matrix.CreateScale(_scale);
        _transform = position * offset * scale;
    }
}
