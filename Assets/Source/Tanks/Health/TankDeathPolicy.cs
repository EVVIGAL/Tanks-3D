using UnityEngine;

[RequireComponent(typeof(GetOutFromWay))]
public class TankDeathPolicy : EnemyDeathPolicy
{
    [SerializeField] private TurretExplosion _turretExplosion;
    [SerializeField] private int _deathLayer;
    [SerializeField] private GameObject[] _physicObjects;

    private GetOutFromWay _getOutFromWay;

    private void Awake()
    {
        _getOutFromWay = GetComponent<GetOutFromWay>();
    }

    protected override void OnDie()
    {
        _turretExplosion.Explose();
        _getOutFromWay.GetOut();

        foreach (GameObject @object in _physicObjects)
            @object.layer = _deathLayer;
    }
}