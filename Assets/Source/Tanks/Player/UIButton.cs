using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onRelease;

    private bool _isPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _onRelease?.Invoke();
    }

    private void Update()
    {
        if (_isPressed)
            _onPressed?.Invoke();
    }
}