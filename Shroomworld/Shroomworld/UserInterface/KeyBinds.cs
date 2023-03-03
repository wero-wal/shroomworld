using System.Collections.Generic;
namespace Shroomworld;
public class KeyBinds {
    // ----- Enums -----
    // ----- Properties -----
    // ----- Fields -----
    private Dictionary<Inputs, Action<>> _bindings;


    // ----- Constructors -----
    public KeyBinds() {
        Initialize();
    }

    // ----- Methods -----
    /// <summary>
    /// Execute the functions bound to the active inputs.
    /// </summary>
    public void ProcessInputs(Inputs[] inputs) {
        foreach (Inputs input in inputs) {
            _bindings[input]();
        }
    }
    public void Initialize() {
        _bindings = new Dictionary<Inputs, Action<>>((int)Inputs.None);
        for (int i = 0; i < (int)Inputs.None; i++) {
            _bindings.Add((Inputs)i, DoNothing);
        }
    }
    public void Add(Inputs input, Action<> onKeyPressed) {
        if (_bindings[input] == DoNothing) {
            _bindings[input] = onKeyPressed;
        }
    }
    public void Remove(Inputs input) {
        _bindings[input] = DoNothing;
    }
    /// <summary>
    /// Called when a key is pressed but has not been bound.
    /// </summary>
    private Action<> DoNothing() {
    }
}