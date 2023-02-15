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
    [SerializeField] private GameOverWindow _gameOverWindow;
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _inputPanel;
    [field: SerializeField] public Wallet PlayerWallet { get; private set; }

    private PlayerTankFactory _playerTankFactory;
    private PlayerTank _playerTank;

    private bool _isGameEnd;

    private void Awake()
    {
        _playerTankFactory = GetComponent<PlayerTankFactory>();
    }

    private void Start()
    {
        _playerTank = _playerTankFactory.CreateTank(_currentTankIndex);
        _playerTank.Init(5, 1000, 20, 100, _healthViewBehaviour, this);
        _artBlowSkill.Init(5);
        _repairKitSkill.Init(2);

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

    private void OnValidate()
    {
        if (_healthViewBehaviour && !(_healthViewBehaviour is IHealthView))
        {
            Debug.LogError(nameof(_healthViewBehaviour) + " needs to implement " + nameof(IHealthView));
            _healthViewBehaviour = null;
        }
    }
}