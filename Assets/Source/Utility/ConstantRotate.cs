using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    private Quaternion _rotation;

    private void Awake()
    {
        _rotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _rotation;
    }
}