using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class SettingSaveLoad : Singleton<SettingSaveLoad>
{
    private MotionBlurSettingUI motionBlurSettingUI;
    private AudioMixer _audioMixer;
    private void Awake()
    {
        motionBlurSettingUI = FindObjectOfType<MotionBlurSettingUI>();
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SaveAudioSetting(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
        AudioState audioState = new AudioState();
        _audioMixer.GetFloat(Audio.Master.ToString(), out float master);
        _audioMixer.GetFloat(Audio.SFX.ToString(), out float sfx);
        _audioMixer.GetFloat(Audio.BGM.ToString(), out float bgm);

        audioState.master = master;
        audioState.sfx = sfx;
        audioState.bgm = bgm;

        string audioJson = JsonUtility.ToJson(audioState);

        string fileName = "Audio";
        string path = Application.dataPath + "/" + fileName + ".json";

        File.WriteAllText(path, audioJson);
    }
    public void LoadAudioSetting(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
        string fileName = "Audio";
        string path = Application.dataPath + "/" + fileName + ".json";
        string json = File.ReadAllText(path);

        AudioState audioState = JsonUtility.FromJson<AudioState>(json);

        _audioMixer.SetFloat("Master", audioState.master);
        _audioMixer.SetFloat("SFX", audioState.sfx);
        _audioMixer.SetFloat("BGM", audioState.bgm);
    }

    public void SaveGraphicSetting()
    {
        GraphicState graphicState = new GraphicState();
        graphicState.graphic = (Graphic)QualitySettings.GetQualityLevel();
        graphicState.motionBlurValue = motionBlurSettingUI.value;
        string graphicJson = JsonUtility.ToJson(graphicState);

        string fileName = "Graphic";
        string path = Application.dataPath + "/" + fileName + ".json";

        File.WriteAllText(path, graphicJson);
    }

    public void LoadGraphicSetting()
    {
        string fileName = "Graphic";
        string path = Application.dataPath + "/" + fileName + ".json";
        string json = File.ReadAllText(path);

        GraphicState graphicState = JsonUtility.FromJson<GraphicState>(json);

        QualitySettings.SetQualityLevel((int)graphicState.graphic);
        motionBlurSettingUI._slider.value = graphicState.motionBlurValue;
        motionBlurSettingUI._text.text = string.Format("{0:0.#}", graphicState.motionBlurValue);

    }


}
