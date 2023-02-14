using UnityEngine;

public class ScrollTrack : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private float _speed = 0.05f;

    private Material _material;
    private float _offset;

    private const string MainTexture = "_MainTex";

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _material = renderer.material;
    }

    private void Update()
    {
        _offset += _movement.CurrentSpeed * _speed * Time.deltaTime;
       // _offset = (_offset + Time.deltaTime * _speed) % 1f;
        _material.SetTextureOffset(MainTexture, new Vector2(_offset, 0f));
    }
}