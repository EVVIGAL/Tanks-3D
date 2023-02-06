using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [field: SerializeField] public float _speed;
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    private ICharacterInputSource _characterInput => (ICharacterInputSource)_inputSourceBehaviour;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var move = new Vector3(0f, 0f, _characterInput.MovementInput.x);
        move *= _speed;
        _characterController.SimpleMove(move);
    }

    private void OnValidate()
    {
        if (_inputSourceBehaviour && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }
}