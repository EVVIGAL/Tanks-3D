using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(OfflineIncome))]
public class MissionPanel : MonoBehaviour
{
    [SerializeField] private SaveData _saveData;
    [SerializeField] private TankFarm[] _farms;

    private OfflineIncome _offlineIncome;

    public event UnityAction IncomeChange;

    public float TotalIncome { get; private set; }

    private void Awake()
    {
        _offlineIncome = GetComponent<OfflineIncome>();
        _offlineIncome.Calculate(_saveData.Data.TotalIncome);

        foreach (TankFarm farm in _farms)
        {
            farm.RateChanged += SetTotalIncome;

            if(farm.Unit.IsAvailable)
                farm.gameObject.SetActive(true);
            else
                farm.gameObject.SetActive(false);
        }

    }

    private void OnEnable()
    {
        SetTotalIncome();
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