using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        UpdateText();
    }

    public void UpdateText()
    {
        text.text = $"Gold : {ShopState.Instance.Gold}";
    }
}
