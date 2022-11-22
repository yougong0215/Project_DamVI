using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public enum Graphic
{
    ������,
    ����,
    ��������,
    ������,
    ����,
    ��������
}

public class GraphicSettingUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Slider _slider;
    [SerializeField]
    private Graphic _graphic;

    
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _slider = GetComponentInChildren<Slider>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = 5;
        _slider.minValue = 0;


        _slider.value = QualitySettings.GetQualityLevel();
        _graphic = (Graphic)QualitySettings.GetQualityLevel();
        _text.text = _graphic.ToString();
    }

    void Load()
    {
    }

    public void ChangeValue(float n)
    {
        int i = (int)n;
        _graphic = (Graphic)i;
        _text.text = _graphic.ToString();

        QualitySettings.SetQualityLevel(i);
    }
}
