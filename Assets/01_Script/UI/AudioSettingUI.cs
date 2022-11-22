using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public enum Audio
{
    none,
    Master,
    SFX,
    BGM
}

public class AudioState
{
    public float sfx;
    public float bgm;
    public float master;
}

public class AudioSettingUI : SliderUIBase
{
    private TextMeshProUGUI _text;
    

    public AudioMixer _audioMixer;

    private Slider _slider;
    

    [SerializeField]
    private Audio audioEnum;


    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _slider.minValue = -40;
        _slider.maxValue = 0;
        Load();
    }

    private void Load()
    {
        SettingSaveLoad.Instance.LoadAudioSetting(_audioMixer);

        _audioMixer.GetFloat(audioEnum.ToString(), out float f);
        _slider.value = f;
        _text.text = ((int)(((f - -40) / _slider.minValue) * -100)).ToString();
    }

    public new void ChangeValue(float f)
    {
        if(audioEnum == Audio.none)
        {
            Debug.LogError("Audio Enum is None");
            return;
        }
        _audioMixer.SetFloat(audioEnum.ToString(), f);

        _text.text = ((int)(((f - -40) / _slider.minValue) * -100)).ToString();

        SettingSaveLoad.Instance.SaveAudioSetting(_audioMixer);
    }

   
}
