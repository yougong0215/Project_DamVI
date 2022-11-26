using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCritical : ShopUpgrade
{
    public override void Upgrade()
    {
        base.Upgrade();
        if (!isPossible) return;
        ShopState.Instance.CriticalAdd = shopOption.upValue;
        ShopState.Instance.CriticalCount = shopOption.count;
        UpdateText();
    }

    public override void UpdateText()
    {
        base.UpdateText();
        if (shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{ShopState.Instance.CriticalAdd * 100}%";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{Mathf.Round(ShopState.Instance.CriticalAdd*100)}% ->\n+{Mathf.Round((ShopState.Instance.CriticalAdd+shopOption.upValue)*100)}%";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
