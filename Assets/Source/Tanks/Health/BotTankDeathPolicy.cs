using UnityEngine;

[RequireComponent(typeof(GetOutFromWay))]
public class BotTankDeathPolicy : TankDeathPolicy
{
    private GetOutFromWay _getOutFromWay;

    private void Awake()
    {
        _getOutFromWay = GetComponent<GetOutFromWay>();
    }

    protected override void OnDie()
    {
        _getOutFromWay.GetOut();
    }
}