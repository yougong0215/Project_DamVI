using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBullet : ShopUpgrade
{
    public override void UpgradeStat()
    {
        base.UpgradeStat();
        if (!isPossible) return;
        ShopState.Instance.BulletAdd = (int)shopOption.upValue;
        UpdateText();
    }

    public override void UpdateText()
    {
        if(shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{ShopState.Instance.BulletAdd}";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{ShopState.Instance.BulletAdd} ->\n+{ShopState.Instance.BulletAdd +shopOption.upValue}";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
