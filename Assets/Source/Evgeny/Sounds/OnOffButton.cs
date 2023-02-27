using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class OnOffButton : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        ChangeImage();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeValue);
    }

    private void ChangeValue()
    {
        _audioManager.OnOff();
        ChangeImage();
    }

    private void ChangeImage()
    {
        if (_audioManager.IsMute)
        {
            _image.sprite = _off;
            _image.color = Color.red;
        }

        if (!_audioManager.IsMute)
        {
            _image.sprite = _on;
            _image.color = Color.green;
        }
    }
}