using UnityEngine.Events;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private TankFarm[] _farms;

    public event UnityAction IncomeChange;

    public float TotalIncome { get; private set; }

    private void Awake()
    {
        TotalIncome = 0;

        foreach (TankFarm farm in _farms)
        {
            farm.RateChanged += SetTotalIncome;

            if(farm.Unit.IsAvailable)
                farm.gameObject.SetActive(true);
            else
                farm.gameObject.SetActive(false);
        }

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
            Debug.Log(farm.FarmRate);
            if (farm.gameObject.activeSelf)
                TotalIncome += farm.FarmRate;
        }

        IncomeChange?.Invoke();
    }
}