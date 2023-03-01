using UnityEngine;

public class PointDamage : MonoBehaviour, IDamage
{
    [SerializeField] private DamageView _damageViewTemplate;

    public void TakeDamage(RaycastHit hitInfo, uint damage)
    {
        if (hitInfo.transform.TryGetComponent(out IHealth health))
        {
            if (health.IsAlive)
            {
                uint totalDamage = health.TakeDamage(damage);
                DamageView damageView = Instantiate(_damageViewTemplate, hitInfo.point, Quaternion.identity);
                damageView.Show(totalDamage);
            }
        }
    }
}

public interface IDamage
{
    void TakeDamage(RaycastHit hitInfo, uint damage);
}