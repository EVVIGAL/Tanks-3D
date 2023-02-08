using UnityEngine.Events;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private TankFarm[] _farms;

    public float TotalIncome { get; private set; }

    public event UnityAction IncomeChange;

    private void OnEnable()
    {
        TotalIncome = 0;
        SetTotalIncome();

        foreach (TankFarm farm in _farms)
        {
            farm.RateChanged += SetTotalIncome;

            if(farm.Unit.IsAvailable)
                farm.gameObject.SetActive(true);
            else
                farm.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (TankFarm farm in _farms)
            farm.RateChanged -= SetTotalIncome;
    }

    public void SetTotalIncome()
    {
        foreach (TankFarm farm in _farms)
        {
            if (farm.Unit.IsAvailable)
                TotalIncome += farm.FarmRate;
        }

        IncomeChange?.Invoke();
    }
}