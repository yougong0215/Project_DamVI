using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SettingSaveLoad : Singleton<SettingSaveLoad>
{
    private AudioMixer _audioMixer;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SaveSetting(AudioMixer audioMixer)
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
    public void LoadSetting(AudioMixer audioMixer)
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
}
