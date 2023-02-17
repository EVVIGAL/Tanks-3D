using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    private const uint _maxMedals = 3;

    [SerializeField] private uint _currentMedals;

    public uint CurrentMedals => _currentMedals;

    public void Set(uint medals)
    {
        if(medals <= 0)
            throw new InvalidOperationException();

        if (medals <= _currentMedals)
            return;

        medals = (uint)Mathf.Clamp(medals, 0, _maxMedals);
        _currentMedals = medals;
    }
}