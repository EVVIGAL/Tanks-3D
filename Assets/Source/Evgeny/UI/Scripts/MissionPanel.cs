using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(OfflineIncome))]
public class MissionPanel : MonoBehaviour
{
    [SerializeField] private SaveData _saveData;
    [SerializeField] private TakeButton _takeButton;
    [SerializeField] private TankFarm[] _farms;

    private OfflineIncome _offlineIncome;

    public event UnityAction IncomeChange;

    public float TotalIncome { get; private set; }

    private void Awake()
    {
        _offlineIncome = GetComponent<OfflineIncome>();
    }

    private void OnEnable()
    {
        foreach (TankFarm farm in _farms)
        {
            farm.RateChanged += SetTotalIncome;
            farm.UpdateFarm();

            if(farm.Unit.IsAvailable)
                farm.gameObject.SetActive(true);
            else
                farm.gameObject.SetActive(false);
        }

        SetTotalIncome();
        _offlineIncome.Calculate(_saveData.Data.TotalIncome, _saveData.Data.LastIncome);
        _takeButton.Init();
    }

    private void OnDisable()
    {
        foreach (TankFarm farm in _farms)
            farm.RateChanged -= SetTotalIncome;
    }

    public void SetTotalIncome()
    {
        TotalIncome = 0;

        foreach (TankFarm farm in _farms)
        {
            if (farm.gameObject.activeSelf)
                TotalIncome += farm.FarmRate;
        }

        _saveData.Data.TotalIncome = (int)TotalIncome;
        _saveData.Save();
        IncomeChange?.Invoke();
    }
}