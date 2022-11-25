using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHP : ShopUpgrade
{
    public override void UpgradeStat()
    {
        base.UpgradeStat();
        if (!isPossible) return;
        ShopState.Instance.HPAdd = (int)shopOption.upValue;
        UpdateText();
    }

    public override void UpdateText()
    {
        if(shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{ShopState.Instance.HPAdd}";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{ShopState.Instance.HPAdd} ->\n+{ShopState.Instance.HPAdd+shopOption.upValue}";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
