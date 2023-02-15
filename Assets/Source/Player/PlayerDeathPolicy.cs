using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerDeathPolicy : MonoBehaviour, IDeathPolicy
{
    [SerializeField] private TurretExplosion _turretExplosion;

    private CharacterController _characterController;
    private Root _root;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(Root root)
    {
        _root = root;
    }

    public void Die()
    {
        _turretExplosion.Explose();
        _characterController.enabled = false;

        if (_root)
            _root.GameOver();
    }
}