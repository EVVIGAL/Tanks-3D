using UnityEngine.SceneManagement;
using System.Collections;
using Cinemachine;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerTankFactory))]
public class Root : MonoBehaviour
{
    [SerializeField] private FinalReward _reward;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private DamageCounter _damageCounter;
    [SerializeField] private SaveData _data;
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Skill _artBlowSkill;
    [SerializeField] private Skill _repairKitSkill;
    [SerializeField] private MobileInputUI _mobileInputUI;
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

        _data.Data.ArtilleryAmount -= (int)SetSkillAmount(_artBlowSkill, (uint)_data.Data.ArtilleryAmount);
        _data.Data.ToolsAmount -= (int)SetSkillAmount(_repairKitSkill, (uint)_data.Data.ToolsAmount);
        _damageCounter.Init(_unit.Health.Value, _currentLevelIndex);
        _levelText.text = (_currentLevelIndex - 1).ToString();
        _reward.Init((int)_currentLevelIndex - 1, (int)_data.Data.Levels[_currentLevelIndex - 2].CurrentMedals);
        PlayerWallet.Init((uint)_data.Data.Money);
    }

    public void CreatePlayerTank()
    {
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
        if (_isGameEnd)
            return;

        _isGameEnd = true;
        StartCoroutine(CompleteLevel());
    }

    public void GameOver()
    {
        if (_isGameEnd)
            return;

        _gameOverWindow.gameObject.SetActive(true);
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

    private IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(_waitTime);
        _levelCompletedWindow.gameObject.SetActive(true);
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