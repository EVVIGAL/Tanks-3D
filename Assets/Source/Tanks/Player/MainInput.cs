using UnityEngine;

public class MainInput : MonoBehaviour, ICharacterInputSource
{
    [SerializeField] private MonoBehaviour _uiInputBehaviour;
    private ICharacterInputSource _uiInput => (ICharacterInputSource)_uiInputBehaviour;

    [SerializeField] private MonoBehaviour _playerInputBehaviour;
    private ICharacterInputSource _playerInput => (ICharacterInputSource)_playerInputBehaviour;

    public Vector2 MovementInput { get; private set; }

    private void Update()
    {
        MovementInput = _uiInput.MovementInput + _playerInput.MovementInput;
        MovementInput.Normalize();
    }

    private void OnValidate()
    {
        if (_uiInputBehaviour && !(_uiInputBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_uiInputBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _uiInputBehaviour = null;
        }

        if (_playerInputBehaviour && !(_playerInputBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_playerInputBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _playerInputBehaviour = null;
        }
    }
}