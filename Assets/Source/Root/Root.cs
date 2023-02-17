using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerTankFactory))]
public class Root : MonoBehaviour
{
    [SerializeField] private uint _currentTankIndex;
    [SerializeField] private SaveData _data;
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Skill _artBlowSkill;
    [SerializeField] private Skill _repairKitSkill;
    [SerializeField] private MobileInputUI _mobileInputUI;
    [SerializeField] private GameOverWindow _gameOverWindow;
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _inputPanel;
    [field: SerializeField] public Wallet PlayerWallet { get; private set; }

    private PlayerTankFactory _playerTankFactory;
    private PlayerTank _playerTank;
    private UnitStat _unit;

    private bool _isGameEnd;

    private void Awake()
    {
        _playerTankFactory = GetComponent<PlayerTankFactory>();
        LoadStats();
    }

    private void Start()
    {
        _playerTank = _playerTankFactory.CreateTank((uint)_data.Data.CurrentTankIndex);
        _playerTank.Init(_unit.Speed.Value, (uint)_unit.Health.Value, (uint)_unit.Armor.Value, (uint)_unit.Damage.Value, _healthViewBehaviour, this);
        _artBlowSkill.Init(_data.Data.ArtilleryAmount);
        _repairKitSkill.Init(_data.Data.ToolsAmount);

        _virtualCamera.Follow = _playerTank.transform;
        _virtualCamera.LookAt = _playerTank.transform;
        _mobileInputUI.Init(_playerTank.UIInput);
    }

    public void LevelCompleted()
    {
        if (_isGameEnd)
            return;

        _levelCompletedWindow.gameObject.SetActive(true);
        EndGame();
    }

    public void GameOver()
    {
        if (_isGameEnd)
            return;

        _gameOverWindow.gameObject.SetActive(true);
        EndGame();
    }

    private void EndGame()
    {
        _playerTank.Stop();
        _inputPanel.SetActive(false);
        _isGameEnd = true;
    }

    private void LoadStats()
    {
        if (!PlayerPrefs.HasKey(_data.Data.CurrentTankName))
            return;

        _unit = SaveManager.Load<UnitStat>(_data.Data.CurrentTankName);
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