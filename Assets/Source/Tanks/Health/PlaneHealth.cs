using System.Collections;
using UnityEngine;

[RequireComponent (typeof(SmokeDamageView))]
public class PlaneHealth : BotHealth
{
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    [SerializeField] private SmokeDamageView[] _smokeDamageView;
    [SerializeField] private Rotater[] _propellers;

    [SerializeField] private float _explosionRange = 5f;
    [SerializeField] private float _explosionDelay = 1f;
    [SerializeField] private uint _explosionCount = 3;
    [SerializeField] private ParticleSystem _explosionVfx;

    protected override void OnTakeDamage()
    {
        _healthView.Show(Value, MaxValue);
        foreach (SmokeDamageView smokeView in _smokeDamageView)
            smokeView.Show(Value, MaxValue);
    }

    protected override void Die()
    {
        base.Die();
        foreach (Rotater rotater in _propellers)
            rotater.enabled = false;

        StartCoroutine(Explosion());
    }

    private void OnValidate()
    {
        if (_healthViewBehaviour && !(_healthViewBehaviour is IHealthView))
        {
            Debug.LogError(nameof(_healthViewBehaviour) + " needs to implement " + nameof(IHealthView));
            _healthViewBehaviour = null;
        }
    }

    private IEnumerator Explosion()
    {
        for (int i = 0; i < _explosionCount; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * _explosionRange;
            Vector3 explosionPosition = transform.position + randomOffset;
            Instantiate(_explosionVfx, explosionPosition, Quaternion.identity);
            yield return new WaitForSeconds(_explosionDelay);
        }
    }
}