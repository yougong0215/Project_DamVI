using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class MotionBlurSettingUI :  SliderUIBase
{
    [SerializeField]
    private Volume volume;
    public MotionBlur _motionBlur;

    public float maxValue;


   
    public float value;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _slider = GetComponentInChildren<Slider>();
        volume = GameObject.FindObjectOfType<Volume>();
        _slider.minValue = 0;
         _slider.maxValue = 1;
        volume.profile.TryGet<MotionBlur>(out _motionBlur);
        
        SettingSaveLoad.Instance.LoadGraphicSetting();
        _motionBlur.intensity.value = Mathf.Lerp(0, maxValue, value);
    }

    public new void ChangeValue(float f)
    {
        value = f;
        _motionBlur.intensity.value = Mathf.Lerp(0, maxValue, value);
        //_text.text = value.ToString();
        _text.text = string.Format("{0:0.#}", value);
        
        SettingSaveLoad.Instance.SaveGraphicSetting();
    }
}
