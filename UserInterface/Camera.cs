using System;
using Microsoft.Xna.Framework;

namespace Shroomworld;
public class Camera {
    private Vector2 _position;
    private Vector2 _centreOfScreen;
    private Rectangle _movementBounds;

    public Camera(Rectangle window, int offset) {
        UpdateMovementBounds(offset, window);
        _position = new Vector2(100, 100);
    }

    public void UpdateMovementBounds(int offset, Rectangle window) {
        Point offsetPoint = new Point(offset, offset);
        _movementBounds = new Rectangle(window.Location + offsetPoint, window.Size - offsetPoint - offsetPoint);
        _centreOfScreen = new Vector2((window.Left + window.Right) / 2, (window.Top + window.Bottom) / 2);
    }
    public Vector2 CalibratePosition(Vector2 rawPosition) {
        // Find the vector from the camera to the position and add it to the centre of the screen.
        return _centreOfScreen + rawPosition - _position;
    }
    public void Move(Rectangle playerHitbox) {
        _position.X -= Math.Max(0, _movementBounds.Left - playerHitbox.Left);
        _position.X += Math.Max(0, playerHitbox.Right - _movementBounds.Right);
        _position.Y -= Math.Max(0, _movementBounds.Top - playerHitbox.Top);
        _position.Y += Math.Max(0, playerHitbox.Bottom - _movementBounds.Bottom);
    }
}