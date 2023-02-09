using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class HideButton : MonoBehaviour
{
    [SerializeField] private HideType _hideType;
    [SerializeField] private float _delayTime;
    [SerializeField] private GameObject[] _objectsToHide;

    private Button _button;

    private enum HideType
    {
        Hide,
        Unhide
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Hide);
    }

    private void Hide()
    {
        Invoke(nameof(SetActive), _delayTime);
    }

    private void SetActive()
    {
        foreach (GameObject obj in _objectsToHide)
        {
            if (obj != null)
                obj.SetActive(_hideType == HideType.Unhide);
        }
    }
}