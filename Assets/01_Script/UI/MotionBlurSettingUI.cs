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

    public TextMeshProUGUI _text;

    public Slider _slider;
   
    [HideInInspector]
    public float value;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _slider = GetComponentInChildren<Slider>();
        _slider.minValue = 0;
        _slider.maxValue = 1;
    }
    void Start()
    {
        volume = GameObject.FindObjectOfType<Volume>();
        if(volume.profile.TryGet<MotionBlur>(out MotionBlur tmp))
        {
            _motionBlur = tmp;
        }
        SettingSaveLoad.Instance.LoadGraphicSetting();
        _motionBlur.intensity.value = Mathf.Lerp(0, maxValue, value);
    }

    public new void ChangeValue(float f)
    {
        value = f;
        _motionBlur.intensity.value = Mathf.Lerp(0, maxValue, value);
        _text.text = value.ToString();
        
        SettingSaveLoad.Instance.SaveGraphicSetting();
    }
}
