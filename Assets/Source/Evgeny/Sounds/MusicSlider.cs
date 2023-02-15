using UnityEngine;

public class MusicSlider : AudioSlider
{
    private void Start()
    {
        Value = AudioManager.MusicValue;
        Value = Mathf.Clamp(Value, Slider.minValue, Slider.maxValue);
        Slider.value = Value;
    }

    public override void Change(float value)
    {
        AudioManager.SetMusic( value);
    }
}