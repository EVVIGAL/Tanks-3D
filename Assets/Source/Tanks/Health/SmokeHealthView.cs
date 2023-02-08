using UnityEngine;

[RequireComponent(typeof(SmokeDamageView))]
public class SmokeHealthView : MonoBehaviour, IHealthView
{
    [SerializeField] private MonoBehaviour _healthViewBehaviour;
    private IHealthView _healthView => (IHealthView)_healthViewBehaviour;

    private SmokeDamageView _smokeDamageView;

    private void Awake()
    {
        _smokeDamageView = GetComponent<SmokeDamageView>();
    }

    public void Show(float health, float maxHealth)
    {
        _healthView.Show(health, maxHealth);
        _smokeDamageView.Show(health, maxHealth);
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