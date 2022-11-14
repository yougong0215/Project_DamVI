using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    private Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0;
        _slider.maxValue = 1;
    }
    public void ChangeValue(float value)
    {
        Time.timeScale = value;
        Debug.Log(value);
    }
}
