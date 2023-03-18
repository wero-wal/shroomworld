using Microsoft.Xna.Framework;

namespace Shroomworld;
public class Camera {
    private Vector2 _position;
    private Vector2 _centreOfScreen;

    public Camera(Rectangle window) {
        UpdateCentreOfScreen(window);
        _position = _centreOfScreen;
    }

    public void UpdateCentreOfScreen(Rectangle windowBounds) {
        _centreOfScreen = new Vector2((windowBounds.Left + windowBounds.Right) / 2, (windowBounds.Top + windowBounds.Bottom) / 2);
    }
    public Vector2 CalibratePosition(Vector2 rawPosition) {
        // Find the vector from the camera to the position and add it to the centre of the screen.
        return _centreOfScreen + rawPosition - _position;
    }
    public void MoveToPlayer(Rectangle playerHitbox) {
        _position = playerHitbox.Location.ToVector2();
    }
}