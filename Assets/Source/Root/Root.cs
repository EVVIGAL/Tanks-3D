using UnityEngine.SceneManagement;
using System.Collections;
using GameAnalyticsSDK;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerTankFactory))]
public class Root : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private FinalReward _reward;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private DamageCounter _damageCounter;
    [SerializeField] private SaveData _data;
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Skill _artBlowSkill;
    [SerializeField] private Skill _repairKitSkill;
    [SerializeField] private MobileInputUI _mobileInputUI;
    [SerializeField] private GamePausedWindow _gamePauseWindow;
    [SerializeField] private GameOverWindow _gameOverWindow;
    [SerializeField] private LevelCompletedWindow _levelCompletedWindow;
    [SerializeField] private GameObject _inputPanel;
    [SerializeField] private WeaponReloaderView _weaponReloaderView;
    [field: SerializeField] public Wallet PlayerWallet { get; private set; }

    private const float _waitTime = 2.5f;
    private const uint _skillAmount = 3;

    private PlayerTankFactory _playerTankFactory;
    private PlayerTank _playerTank;
    private UnitStat _unit;

    private uint _currentTankIndex;
    private uint _currentLevelIndex;

    private bool _isGameEnd;

    public DamageCounter DamageCounter => _damageCounter;

    public Skill RepairSkill => _repairKitSkill;

    public Skill ArtBlowSkill => _artBlowSkill;

    public uint CurrentLevelIndex => _currentLevelIndex;

    private void Awake()
    {
        _playerTankFactory = GetComponent<PlayerTankFactory>();
        _currentLevelIndex = (uint)SceneManager.GetSceneAt(0).buildIndex;
    }

    private void Start()
    {
        if (!_isGameEnd)
            CreatePlayerTank();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, (_currentLevelIndex - 1).ToString() + " - level started");
        _data.Data.ArtilleryAmount -= _gamePauseWindow.SetArtilleryAmount((int)SetSkillAmount(_artBlowSkill, (uint)_data.Data.ArtilleryAmount));
        _data.Data.ToolsAmount -= _gamePauseWindow.SetToolsAmount((int)SetSkillAmount(_repairKitSkill, (uint)_data.Data.ToolsAmount));
        _damageCounter.Init(_unit.Health.Value, _currentLevelIndex);
        _levelText.text = (_currentLevelIndex - 1).ToString();
        _reward.Init((int)_currentLevelIndex - 1, (int)_data.Data.Levels[_currentLevelIndex - 2].CurrentMedals);
        PlayerWallet.Init((uint)_data.Data.Money);
    }

    public void CreatePlayerTank()
    {
        _pauseButton.interactable = true;
        Vector3 instancePosition = Vector3.zero;

        if (_playerTank != null)
        {
            instancePosition = _playerTank.transform.position;
            Destroy(_playerTank.gameObject);
        }

        _playerTank = _playerTankFactory.CreateTank(_currentTankIndex, instancePosition);
        _playerTank.Init((float)_unit.Speed.Value, (uint)_unit.Health.Value, (uint)_unit.Armor.Value, (uint)_unit.Damage.Value, _healthViewBehaviour, this, _artBlowSkill, _repairKitSkill, _weaponReloaderView);
        _virtualCamera.Follow = _playerTank.transform;
        _virtualCamera.LookAt = _playerTank.transform;
        _mobileInputUI.Init(_playerTank.UIInput);
        _isGameEnd = false;
    }

    public void LevelCompleted()
    {
        _pauseButton.interactable = false;

        if (_isGameEnd)
            return;

        _isGameEnd = true;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, (_currentLevelIndex - 1).ToString() + " - level complete");
        StartCoroutine(CompleteLevel(_levelCompletedWindow.gameObject));
    }

    public void GameOver()
    {
        _pauseButton.interactable = false;

        if (_isGameEnd)
            return;

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, (_currentLevelIndex - 1).ToString() + " - level fail");
        StartCoroutine(CompleteLevel(_gameOverWindow.gameObject));
        EndGame();
    }

    public void Init(UnitStat unit, uint index)
    {
        _unit = unit;
        _currentTankIndex = index;
    }

    private uint SetSkillAmount(Skill skill, uint amount)
    {
        if(_skillAmount > amount)
        {
            skill.Init((int)amount);
            return amount;
        }

        skill.Init((int)_skillAmount);
        return _skillAmount;
    }

    private void EndGame()
    {
        _playerTank.Stop();
        _inputPanel.SetActive(false);
        _isGameEnd = true;
    }

    private IEnumerator CompleteLevel(GameObject endPanel)
    {
        yield return new WaitForSeconds(_waitTime);
        endPanel.SetActive(true);
        EndGame();
        yield break;
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