using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [System.Serializable]
    public class Event : UnityEvent<Vector2> { }

    [Header("Rect References")]
    [SerializeField] private RectTransform _containerRect;
    [SerializeField] private RectTransform _handleRect;

    [Header("Settings")]
    [SerializeField] private float _joystickRange = 50f;
    [SerializeField] private float _magnitudeMultiplier = 1f;

    [Header("Output")]
    [SerializeField] private Event _joystickOutputEvent;

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_containerRect, eventData.position, eventData.pressEventCamera, out Vector2 position);
        position = ApplySizeDelta(position);
        Vector2 clampedPosition = ClampValuesToMagnitude(position);
        OutputPointerEventValue(position * _magnitudeMultiplier);
        _handleRect.anchoredPosition = clampedPosition * _joystickRange;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputPointerEventValue(Vector2.zero);
        _handleRect.anchoredPosition = Vector2.zero;
    }

    private void OutputPointerEventValue(Vector2 pointerPosition)
    {
        _joystickOutputEvent.Invoke(pointerPosition);
    }

    private Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x / _containerRect.sizeDelta.x) * 2.5f;
        float y = (position.y / _containerRect.sizeDelta.y) * 2.5f;
        return new Vector2(x, y);
    }

    private Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }
}