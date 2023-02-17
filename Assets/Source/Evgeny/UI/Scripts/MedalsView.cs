using UnityEngine;
using System;

public class MedalsView : MonoBehaviour
{
    [SerializeField] private Medal[] _medals;

    public void Show(uint count)
    {
        if(count <= 0)
            throw new InvalidOperationException();

        count = (uint)Mathf.Clamp(count, 0, _medals.Length);

        for (int i = 0; i < count; i++)
            _medals[i].gameObject.SetActive(true);
    }
}