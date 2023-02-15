using UnityEngine;

public class EffectsSlider : AudioSlider
{
    private void Start()
    {
        Value = AudioManager.EffectsValue;
        Value = Mathf.Clamp(Value, Slider.minValue, Slider.maxValue);
        Slider.value = Value;
    }

    public override void Change(float value)
    {
        AudioManager.SetEffects(value);
    }
}