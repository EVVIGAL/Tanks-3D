using UnityEngine.UI;
using UnityEngine;
using System;

public class MedalsView : MonoBehaviour
{
    [SerializeField] private Image[] _medals;

    public void Show(uint count)
    {
        Debug.Log("Here" + count);
        if (count <= 0)
            throw new InvalidOperationException();

        count = (uint)Mathf.Clamp(count, 0, _medals.Length);

        for (int i = 0; i < count; i++)
            _medals[i].color = Color.yellow;
    }
}