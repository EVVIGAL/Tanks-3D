using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerTankFactory))]
public class Root : MonoBehaviour
{
    [SerializeField] private uint _currentTankIndex;
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Skill _artBlowSkill;
    [SerializeField] private Skill _repairKitSkill;
    [SerializeField] private MobileInputUI _mobileInputUI;

    private PlayerTankFactory _playerTankFactory;

    private void Awake()
    {
        _playerTankFactory = GetComponent<PlayerTankFactory>();
    }

    private void Start()
    {
        PlayerTank tank = _playerTankFactory.CreateTank(_currentTankIndex);
        tank.Init(5, 1000, 20, 100, _healthViewBehaviour);
        _artBlowSkill.Init(5);
        _repairKitSkill.Init(2);

        _virtualCamera.Follow = tank.transform;
        _virtualCamera.LookAt = tank.transform;
        _mobileInputUI.Init(tank.UIInput);
    }

    private void OnValidate()
    {
        if (_healthViewBehaviour && !(_healthViewBehaviour is IHealthView))
        {
            Debug.LogError(nameof(_healthViewBehaviour) + " needs to implement " + nameof(IHealthView));
            _healthViewBehaviour = null;
        }
    }
}