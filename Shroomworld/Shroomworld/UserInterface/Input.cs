using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Shroomworld;
public static class Input {

    // ----- Enums -----
    public enum Inputs {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        N0, N1, N2, N3, N4, N5, N6, N7, N8, N9,
        Escape, Enter, Space,
        RightArrow, LeftArrow, UpArrow, DownArrow,
        LeftMouseButtonDown, RightMouseButtonDown,
        None
    }

    // ----- Fields -----
    private static MouseState _currentMouseState;
    private static MouseState _previousMouseState;
    private static KeyboardState _currentKeyboardState;
    private static KeyboardState _previousKeyboardState;

    private static Dictionary<Keys, Inputs> _miscKeysToInputs = new() {
        { Keys.Escape, Inputs.Escape },
        { Keys.Enter, Inputs.Enter },
        { Keys.Space, Inputs.Space },
        { Keys.Right, Inputs.RightArrow },
        { Keys.Left, Inputs.LeftArrow },
        { Keys.Up, Inputs.UpArrow },
        { Keys.Down, Inputs.DownArrow }
    };


    // ----- Methods -----
    public static Vector2 GetMousePosition() {
        return _currentMouseState.Position.ToVector2();
    }
    public static Inputs[] GetInputs() {
        List<Inputs> inputs = new(capacity: (int) Inputs.None);
        Inputs key;

        // Convert key presses to Inputs.
        foreach (Keys pressedKey in _currentKeyboardState.GetPressedKeys()) {
            key = pressedKey.ToInputs();
            if (key != Inputs.None) {
                inputs.Add(key);
            }
        }
        // Convert mouse button presses to Inputs.
        if (_currentMouseState.LeftButton.HasFlag(ButtonState.Pressed)) {
            inputs.Add(Inputs.LeftMouseButtonDown);
        }
        if (_currentMouseState.RightButton.HasFlag(ButtonState.Pressed)) {
            inputs.Add(Inputs.RightMouseButtonDown);
        }

        return inputs.ToArray();
    }
    public static void Update() {
        _previousMouseState = _currentMouseState;
        _previousKeyboardState = _currentKeyboardState;
        _currentMouseState = Mouse.GetState();
        _currentKeyboardState = Keyboard.GetState();
    }
    
    private static Inputs ToInputs(this Keys key) {
        // Letters
        if ((key >= Keys.A) && (key <= Keys.Z)) {
            return (Inputs)((int)key - (int)Keys.A + (int)Inputs.A);
        }
        // Numbers
        else if ((key >= Keys.D0) && (key <= Keys.D9))  {
            return (Inputs)((int)key - (int)Keys.D0 + (int)Inputs.N0);
        }
        // Other
        else {
            try {
                return _miscKeysToInputs[key];
            }
            catch (Exception) {
                // Unknown
                return Inputs.None;
            }
        }
    }
}
