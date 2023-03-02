using System.Collections.Generic;
namespace Shroomworld;
public class KeyBinds {
    // ----- Enums -----
    public enum Inputs {
        Jump, MoveLeft, MoveRight,
        AttackOrBreak, InteractOrPlace,
        OpenPauseMenu, OpenQuestMenu, OpenInventory,
        HotbarSlot1, HotbarSlot2, HotbarSlot3, HotbarSlot4,
        None
    }

    // ----- Properties -----
    // ----- Fields -----
    public delegate Action<> OnKeyPressed();

    private Dictionary<Inputs, OnKeyPressed> _bindings;

    // ----- Constructors -----
    public KeyBinds() {
        Initialize();
    }

    // ----- Methods -----
    // Todo: initialize method where all keys are bound to an error method
    public void ProcessInput(Inputs input) {
        _bindings[input]();
    }
    public void Initialize() {
        _bindings = new Dictionary<Inputs, OnKeyPressed>((int)Inputs.None);
        for (int i = 0; i < (int)Inputs.None; i++) {
            _bindings.Add((Inputs)i, Error);
        }
    }
    public void Add(Inputs input, OnKeyPressed onKeyPressed) {
        if (_bindings[input] == Error) {
            _bindings[input] = onKeyPressed;
        }
    }
    public void Remove(Inputs input) {
        _bindings[input] = Error;
    }
    private OnKeyPressed Error() {
        // Key not bound.
        // todo: add error routine
    }
}