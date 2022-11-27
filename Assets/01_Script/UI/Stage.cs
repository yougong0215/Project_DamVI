using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class Stage : MonoBehaviour
{
    public StageEnum stage;
    [SerializeField] string StageName;

    [SerializeField] Image StageScene;
    [SerializeField] TextMeshProUGUI Score;
    [SerializeField] TextMeshProUGUI ClearTime;
    Stageed _stage;


    public void OnEnable()
    {

        try
        {
            string path = Application.dataPath + "/" + StageName + ".json";
            string json;
            json = File.ReadAllText(path);
            _stage = JsonUtility.FromJson<Stageed>(json);

            Score.text = $"최고 점수\n{_stage.Score}";
            ClearTime.text = string.Format("클리어 시간\n{0:00}:{1:00}:{2:00}", _stage.hour, _stage.min, (int)_stage.time);
        }
        catch
        {
            Score.text = $"최고 점수\n없음";
            ClearTime.text = string.Format("클리어 시간\n99:99:99");
        }
    }
}
