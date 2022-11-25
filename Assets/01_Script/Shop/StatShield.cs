using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatShield : ShopUpgrade
{
    public override void UpgradeStat()
    {
        base.UpgradeStat();
        if (!isPossible) return;
        ShopState.Instance.ShieldAdd = (int)shopOption.upValue;
        UpdateText();
    }

    public override void UpdateText()
    {
        if(shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{ShopState.Instance.ShieldAdd}";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{ShopState.Instance.ShieldAdd} ->\n+{ShopState.Instance.ShieldAdd +shopOption.upValue}";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
