using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class Stageed
{
    public float time = 99;
    public int min = 99;
    public int hour = 99;

    public int Score = 0;
}

public class StageClear : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject UI;
    [SerializeField] TextMeshProUGUI Times;
    [SerializeField] TextMeshProUGUI Scores;
    [SerializeField] TextMeshProUGUI Gold;

    [Header("????")]
    [SerializeField] int MaxGold = 100;
    [SerializeField] int MinGold = 0;
    GameObject obj;

    float time = 0;
    int min = 0;
    int hour = 0;
    int Score = 0;
    bool a =false;

    public bool infin = false;

    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }
    Stageed _stage;

    string sceneName;


    private void Update()
    {
        if(a==true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Clear;
        }
    }

    public void OnDied()
    {
        a = true;
        if(infin == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            OnClear();
            return;
        }

        if (a == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            a = true;
        }
        sceneName = SceneManager.GetActiveScene().name;

        obj = Instantiate(UI);
        Times = obj.transform.Find("Time").GetComponent<TextMeshProUGUI>();
        Scores = obj.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        Gold = obj.transform.Find("Gold").GetComponent<TextMeshProUGUI>();

        time = PlayerAttackManager.Instance.CurrentClearTime;
        Score = PlayerAttackManager.Instance.CurrentScore;


        min = (int)time / 60;
        hour = min / 60;
        time %= 60;
        Times.text = $"?÷??? ?ð? : {hour}:{min}:{(int)time}";
        Gold.text = $"ȹ?? ???? : ????";
        Scores.text = $"ȹ?? ???? : {Score}";
    }
    public void OnClear()
    {
        if (a == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            a = true;
        }
        PlayerAttackManager.Instance._ani.enabled = false;
        PlayerAttackManager.Instance.StopAllCoroutines();
        sceneName = SceneManager.GetActiveScene().name;

        obj = Instantiate(UI);
        Times = obj.transform.Find("Time").GetComponent<TextMeshProUGUI>();
        Scores = obj.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        Gold = obj.transform.Find("Gold").GetComponent<TextMeshProUGUI>();

        time = PlayerAttackManager.Instance.CurrentClearTime;
        Score = PlayerAttackManager.Instance.CurrentScore;

        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Clear;

        Gold.text = $"ȹ?? ???? : {(int)(Score/time)}";

        ShopState.Instance.Gold = (int)(Score / time);

        min = (int)time / 60;
        hour = min / 60;
        time %= 60;

        Save();


        Times.text = $"?÷??? ?ð? : {hour}:{min}:{(int)time}";
        Scores.text = $"ȹ?? ???? : {Score}";
    }

    public void Save()
    {
        string path = Application.dataPath + "/" + sceneName + ".json";
        string json;
        Debug.Log(Application.dataPath + "/" + sceneName + ".json");
        try
        {
            json = File.ReadAllText(path);
            _stage = JsonUtility.FromJson<Stageed>(json);
        }
        catch
        {
            _stage = new Stageed();
        }

        if(_stage.hour >= hour)
            if(_stage.min >= min) 
                if(_stage.time >= time)
                {
                    _stage.time = time;
                    _stage.hour = hour;
                    _stage.min = min;
                }

        if(Score >= _stage.Score)
        {
            _stage.Score = Score;
        }

        json = JsonUtility.ToJson(_stage, true);
        File.WriteAllText(path, json);

    }
}
