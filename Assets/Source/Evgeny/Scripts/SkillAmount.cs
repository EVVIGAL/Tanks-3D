using UnityEngine;
using TMPro;

public abstract class SkillAmount : MonoBehaviour
{
    [SerializeField] protected SaveData _saveData;

    protected int _amount;
    protected TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        Set();
    }

    public void Add(int amount)
    {
        _amount += amount;
        Set();
        Save();
    }

    public virtual void Save()
    {
    }

    public virtual void Set()
    {
    }
}