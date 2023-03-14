using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Shroomworld;
public static class Input {

    // ----- Enums -----
    [DefaultValue(Inputs.None)]
    public enum Inputs {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        N0, N1, N2, N3, N4, N5, N6, N7, N8, N9,
        Escape, Enter, Space,
        RightArrow, LeftArrow, UpArrow, DownArrow,
        LeftMouseButtonDown, RightMouseButtonDown,
        None
    }

    // ----- Properties -----
    public static Inputs[] CurrentInputs => _currentInputs.ToArray();

    // ----- Fields -----
    private static MouseState _mouseState;
    private static KeyboardState _keyboardState;
    private static List<Inputs> _currentInputs;
    private static Inputs[] _previousInputs;

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
    public static void Initialise() {
        _currentInputs = new Inputs(capacity: (int)Inputs.None);
    }
    public static void Update() {
        _mouseState = Mouse.GetState();
        _keyboardState = Keyboard.GetState();
        _previousInputs = _currentInputs;
        // Update current inputs.
        ExtractInputsFromStates();
    }
    public static bool HasBeenReleased(Inputs input) {
        return _currentInputs.Contains(input)
        && (!_previousInputs.Contains(input));
    }
    public static Vector2 GetMousePosition() {
        return _mouseState.Position.ToVector2();
    }

    private static Inputs[] ExtractInputsFromStates() {
        _currentInputs.Clear();

        // Convert key presses to Inputs.
        foreach (Keys pressedKey in _keyboardState.GetPressedKeys()) {
            key = pressedKey.ToInputs();
            if (key != Inputs.None) {
                _currentInputs.Add(key);
            }
        }
        // Convert mouse button presses to Inputs.
        if (_mouseState.LeftButton.HasFlag(ButtonState.Pressed)) {
            _currentInputs.Add(Inputs.LeftMouseButtonDown);
        }
        if (_mouseState.RightButton.HasFlag(ButtonState.Pressed)) {
            _currentInputs.Add(Inputs.RightMouseButtonDown);
        }
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
