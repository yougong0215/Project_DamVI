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

    [SerializeField]
    private List<string> fileNames;
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
        motionBlurSettingUI = FindObjectOfType<MotionBlurSettingUI>();
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
        motionBlurSettingUI = FindObjectOfType<MotionBlurSettingUI>();
        string fileName = "Graphic";
        string path = Application.dataPath + "/" + fileName + ".json";
        string json = File.ReadAllText(path);

        GraphicState graphicState = JsonUtility.FromJson<GraphicState>(json);

        QualitySettings.SetQualityLevel((int)graphicState.graphic);
        motionBlurSettingUI._slider.value = graphicState.motionBlurValue;
        motionBlurSettingUI._text.text = string.Format("{0:0.#}", graphicState.motionBlurValue);

    }

    public void SaveShop()
    {
        ShopStateForSave shopState = new ShopStateForSave();

        #region 노가다의 산물
        shopState.S_attack = ShopState.Instance.AttackAdd;
        shopState.S_critical = ShopState.Instance.CriticalAdd;
        shopState.S_Bullet = ShopState.Instance.BulletAdd;
        shopState.S_hp = ShopState.Instance.HPAdd;
        shopState.S_Mp = ShopState.Instance.MPAdd;
        shopState.S_Shield = ShopState.Instance.ShieldAdd;
        shopState.whill = ShopState.Instance.Willadd;
        shopState.shoot = ShopState.Instance.ShooGun;
        shopState.quick = ShopState.Instance.QuickDrowBool;
        shopState.special = ShopState.Instance.UltBool;

        shopState.attackCount = ShopState.Instance.AttackCount;
        shopState.criticalCount = ShopState.Instance.CriticalCount;
        shopState.bulletCount = ShopState.Instance.BulletCount;
        shopState.hpCount = ShopState.Instance.HPCount;
        shopState.mpCount = ShopState.Instance.MPCount;
        shopState.shieldCount = ShopState.Instance.ShieldCount;
        shopState.whillCount=ShopState.Instance.WhillCount;
        shopState.shootCount=ShopState.Instance.ShootCount;
        shopState.quickCount=ShopState.Instance.QuickCount;
        shopState.specialCount=ShopState.Instance.SpecialCount;

        shopState.S_gold = ShopState.Instance.Gold;
        #endregion

        string shopJson = JsonUtility.ToJson(shopState);

        string fileName = "Shop";
        string path = Application.dataPath + "/" + fileName + ".json";

        File.WriteAllText(path, shopJson);
        Debug.Log("저장완료");
    }

    public void LoadShop()
    {
        try
        {
            string fileName = "Shop";
            string path = Application.dataPath + "/" + fileName + ".json";
            string json = File.ReadAllText(path);

            ShopStateForSave shopState = JsonUtility.FromJson<ShopStateForSave>(json);

            #region 노가다의 산물
            ShopState.Instance.AttackAdd = shopState.S_attack;
            ShopState.Instance.CriticalAdd = shopState.S_critical;
            ShopState.Instance.BulletAdd = shopState.S_Bullet;
            ShopState.Instance.HPAdd = shopState.S_hp;
            ShopState.Instance.MPAdd = shopState.S_Mp;
            ShopState.Instance.ShieldAdd = shopState.S_Shield;
            ShopState.Instance.Willadd = shopState.whill;
            ShopState.Instance.ShooGun = shopState.shoot;
            ShopState.Instance.QuickDrowBool = shopState.quick;
            ShopState.Instance.UltBool = shopState.special;

            ShopState.Instance.AttackCount = shopState.attackCount;
            ShopState.Instance.CriticalCount = shopState.criticalCount;
            ShopState.Instance.BulletCount = shopState.bulletCount;
            ShopState.Instance.HPCount = shopState.hpCount;
            ShopState.Instance.MPCount = shopState.mpCount;
            ShopState.Instance.ShieldCount = shopState.shieldCount;
            ShopState.Instance.WhillCount = shopState.whillCount;
            ShopState.Instance.ShootCount = shopState.shootCount;
            ShopState.Instance.QuickCount = shopState.quickCount;
            ShopState.Instance.SpecialCount = shopState.specialCount;

            ShopState.Instance.Gold = shopState.S_gold;
            #endregion
        }
        catch
        {

        }



    }
    public void Reset()
    {
        List<string> paths = new List<string>();
        paths.Add(Application.dataPath + "/Graphic.json");
        paths.Add(Application.dataPath + "/Audio.json");
        paths.Add(Application.dataPath + "/Shop.json");

        for(int i = 0; i < paths.Count; i++)
        {
            File.Delete(paths[i]);
        }



    }
}
