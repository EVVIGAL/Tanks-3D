using UnityEngine;

public class PlayerInput : MonoBehaviour, ICharacterInputSource
{
    private Input _input;

    public Vector2 MovementInput { get; private set; }

    private void OnEnable()
    {
        _input = new Input();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        MovementInput = _input.Player.Move.ReadValue<Vector2>();
    }
}