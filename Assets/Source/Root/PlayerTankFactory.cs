using System;
using UnityEngine;

public class PlayerTankFactory : MonoBehaviour
{
    [SerializeField] private PlayerTank[] _tanks;

    public PlayerTank CreateTank(uint index, Vector3 position)
    {
        if (index >= _tanks.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        return Instantiate(_tanks[index], position, Quaternion.LookRotation(Vector3.right));
    }
}