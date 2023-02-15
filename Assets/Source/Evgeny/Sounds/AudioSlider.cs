using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    [SerializeField] protected AudioManager AudioManager;

    protected Slider Slider;
    protected float Value;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Slider.onValueChanged.AddListener(Change);
    }

    private void OnDisable()
    {
        Slider.onValueChanged.RemoveListener(Change);
    }
    public virtual void Change(float value)
    {
    }
}