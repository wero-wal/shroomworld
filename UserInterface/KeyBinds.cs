using System;
using System.Collections.Generic;
namespace Shroomworld;
public class KeyBinds {

    // ----- Enums -----
    // ----- Properties -----

    // ----- Fields -----
    private Dictionary<Input.Inputs, Action> _bindings;


    // ----- Constructors -----
    public KeyBinds() {
        Initialize();
    }

    // ----- Methods -----
    /// <summary>
    /// Execute the functions bound to the active inputs.
    /// </summary>
    public void ProcessInputs(IEnumerable<Input.Inputs> inputs) {
        foreach (Input.Inputs input in inputs) {
            _bindings[input].Invoke();
        }
    }
    /// <summary>
    /// Store all the possible inputs in a dictionary and bind them to <see cref="DoNothing"/>.
    /// </summary>
    public void Initialize() {
        _bindings = new(capacity: (int)Input.Inputs.None);
        for (int i = 0; i < (int)Input.Inputs.None; i++) {
            _bindings.Add((Input.Inputs)i, DoNothing);
        }
    }
    /// <summary>
    /// If an input is not bound to any action, bind it to the specified action.
    /// </summary>
    /// <param name="input">The input (key or mouse button press) you want to bind the action to.</param>
    /// <param name="onKeyPressed">The action to which you want to bind the input.</param>
    public void Add(Input.Inputs input, Action onKeyPressed) {
        if (_bindings[input] == DoNothing) {
            _bindings[input] = onKeyPressed;
        }
    }
    /// <summary>
    /// Binds the <paramref name="input"/> to the function <see cref="DoNothing"/>.
    /// </summary>
    /// <param name="input">The input whose action / binding you want to remove.</param>
    public void Remove(Input.Inputs input) {
        _bindings[input] = DoNothing;
    }
    /// <summary>
    /// Called when a key is pressed but has not been bound.
    /// </summary>
    private void DoNothing() { }
}