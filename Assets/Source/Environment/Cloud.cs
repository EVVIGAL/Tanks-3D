using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        transform.position += Vector3.left * (_moveSpeed * Time.deltaTime);
    }
}