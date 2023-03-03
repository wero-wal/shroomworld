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
        { Keys.RightArrow, Inputs.RightArrow },
        { Keys.LeftArrow, Inputs.LeftArrow },
        { Keys.UpArrow, Inputs.UpArrow },
        { Keys.DownArrow, Inputs.DownArrow }
    };


    // ----- Methods -----
    public Inputs[] GetInputs() {
        Inputs[] inputs = new Inputs[(int)Inputs.None];
        int i = 0;
        Inputs key_Inputs;
        foreach (Keys key in _currentKeyboardState.PressedKeys) {
            key_Inputs = key.ToInputs();
            if (key_Inputs != Inputs.None) {
                inputs[i++] = key_Inputs;
            }
        }
        if (_currentMouseState./*left mouse button down*/) {
            inputs[i++] = Inputs.LeftMouseButtonDown;
        }
        if (_currentMouseState./*right mouse button down*/) {
            inputs[i++] = Inputs.RightMouseButtonDown;
        }
        return inputs;
    }
    public void Update() {
        _currentMouseState = /*new mouse state*/;
        _previousMouseState = _currentMouseState;
        _currentKeyboardState = /*new keyboard state*/;
        _previousKeyboardState = _currentKeyboardState;
    }
    
    private Inputs ToInputs(this Keys key) {
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
