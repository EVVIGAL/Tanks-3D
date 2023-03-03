using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.current;
    }

    private void Update()
    {
        if (_camera == null)
            return;

        transform.position = _camera.transform.position + _offset;
    }
}