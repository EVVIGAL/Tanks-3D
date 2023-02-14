using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    [SerializeField] private UIInput _uIInput;

    public void Init(UIInput uIInput)
    {
        _uIInput = uIInput;
    }

    public void OnMovementJoystickMove(Vector2 delta)
    {
        _uIInput.MoveInput(delta);
    }

    public void OnBarrelJoystickMove(Vector2 delta)
    {
        _uIInput.BarrelRotate(delta);
    }

    public void OnShootButtonClick()
    {
        _uIInput.Shoot();
    }
}