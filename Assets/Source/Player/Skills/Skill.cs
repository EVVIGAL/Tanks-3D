using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour, ISkill
{
    [SerializeField] private int _maxAmount;
    [SerializeField] private float _reloadTime;
    [SerializeField] private SkillView _view;

    private int _currentAmount;

    private void Awake()
    {
        _currentAmount = _maxAmount;
        _view.Show(_currentAmount);
    }

    public void Init(int maxAmount)
    {
        _currentAmount = maxAmount;
        _view.Show(_currentAmount);
    }

    public void Use()
    {
        if (_currentAmount <= 0)
            return;

        OnUse();
        _currentAmount--;
        _view.Show(_currentAmount);

        if (_currentAmount > 0)
            StartCoroutine(Reload());
    }

    protected abstract void OnUse();

    private IEnumerator Reload()
    {
        float runningTime = 0f;
        while(runningTime < _reloadTime)
        {
            runningTime += Time.deltaTime;
            _view.ShowReloadProgress(runningTime / _reloadTime);
            yield return null;
        }
    }
}