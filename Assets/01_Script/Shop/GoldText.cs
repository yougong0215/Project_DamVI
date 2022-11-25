using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private ShopState shopState;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        shopState = ShopState.Instance;
    }
    private void OnEnable()
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        if(shopState == null)
        {
            shopState = ShopState.Instance;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        text.text = $"Gold : {shopState.Gold}";
    }
}
