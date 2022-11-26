using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMP : ShopUpgrade
{
    public override void Upgrade()
    {
        base.Upgrade();
        if (!isPossible) return;
        ShopState.Instance.MPAdd = shopOption.upValue;
        ShopState.Instance.MPCount = shopOption.count;
        UpdateText();
    }

    public override void UpdateText()
    {
        base.UpdateText();
        if (shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{ShopState.Instance.MPAdd}";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{ShopState.Instance.MPAdd} ->\n+{ShopState.Instance.MPAdd +shopOption.upValue}";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
