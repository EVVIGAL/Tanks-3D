using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private int _maxAmount;
    [SerializeField] private SkillView _view;
    [SerializeField] private MonoBehaviour _skillBehaviour;
    private ISkill _skill => (ISkill)_skillBehaviour;

    private int _currentAmount;

    private void Awake()
    {
        _currentAmount = _maxAmount;
        _view.Show(_currentAmount);
    }

    public void Init(int maxAmount)
    {
        _maxAmount = maxAmount;
        _currentAmount = maxAmount;
    }

    public void Use()
    {
        if (_currentAmount <= 0)
            return;

        _skill.Use();
        _currentAmount--;
        _view.Show(_currentAmount);
    }

    private void OnValidate()
    {
        if (_skillBehaviour && !(_skillBehaviour is ISkill))
        {
            Debug.LogError(nameof(_skillBehaviour) + " needs to implement " + nameof(ISkill));
            _skillBehaviour = null;
        }
    }
}